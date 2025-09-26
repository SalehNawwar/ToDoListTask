using Application.Common;
using Application.DTOs.UserDtos;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseDto?> GetByIdAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new EntityNotFoundException(nameof(User), id);

            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                Role = user.Role
            };
        }

        public async Task<PagedResult<UserResponseDto>> GetAllAsync(UserRequestParameters parameters)
        {
            var result = await _userRepository.GetAllAsync(parameters);

            return new PagedResult<UserResponseDto>
            {
                Items = result.Items.Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    UserEmail = u.UserEmail,
                    Role = u.Role
                }),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.UserEmail))
                throw new DuplicateEmailException(dto.UserEmail);

            if (await _userRepository.ExistsByUserNameAsync(dto.UserName))
                throw new DuplicateUserNameException(dto.UserName);

            var user = new User
            {
                UserName = dto.UserName,
                UserEmail = dto.UserEmail,
                Role = dto.Role,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
                // Password hashing is handled separately (AuthService)
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                Role = user.Role
            };
        }

        public async Task UpdateAsync(UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.Id);
            if (user == null)
                throw new EntityNotFoundException(nameof(User), dto.Id);
            
            if (dto.UserEmail != user.UserEmail)
            {
                if (await _userRepository.ExistsByEmailAsync(dto.UserEmail))
                    throw new DuplicateEmailException(dto.UserEmail);

                user.UserEmail = dto.UserEmail;
            }

            if (dto.UserName != user.UserName)
            {
                if (await _userRepository.ExistsByUserNameAsync(dto.UserName))
                    throw new DuplicateUserNameException(dto.UserName);

                user.UserName = dto.UserName;
            }

            user.UpdatedAt = DateTime.Now;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new EntityNotFoundException(nameof(User), id);

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}

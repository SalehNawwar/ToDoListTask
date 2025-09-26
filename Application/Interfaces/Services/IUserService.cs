using Application.Common;
using Application.DTOs.UserDtos;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetByIdAsync(long id);

        Task<PagedResult<UserResponseDto>> GetAllAsync(UserRequestParameters parameters);

        Task<UserResponseDto> CreateAsync(CreateUserDto dto);

        Task UpdateAsync(UpdateUserDto dto);

        Task DeleteAsync(long id);
    }
}
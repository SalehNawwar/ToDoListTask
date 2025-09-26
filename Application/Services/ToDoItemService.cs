using Application.Common;
using Application.DTOs.ToDoItemDtos;
using Application.DTOs.UserDtos;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapping;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IToDoItemRepository _todoRepository;
        private readonly IUserRepository _userRepository;

        public ToDoItemService(IToDoItemRepository todoRepository, IUserRepository userRepository)
        {
            _todoRepository = todoRepository;
            _userRepository = userRepository;
        }

        public async Task<ToDoItemResponseDto?> GetByIdAsync(long id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            if (item == null)
                throw new EntityNotFoundException(nameof(ToDoItem), id);

            return item.ToToDoItemResponseDto();
        }

        public async Task<PagedResult<ListingToDoItemResponseDto>> GetAllAsync(ToDoItemRequestParameters parameters)
        {
            var result = await _todoRepository.GetAllAsync(parameters);

            return new PagedResult<ListingToDoItemResponseDto>
            {
                Items = result.Items.Select(item => item.ToListingToDoItemResponseDto()),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<ToDoItemResponseDto> CreateAsync(CreateToDoItemDto toDoItemDto)
        {
            //var user = await _userRepository.GetByIdAsync(userDto.Id);

            //if (user == null)
            //    throw new EntityNotFoundException(nameof(User), userDto.Id);
            User user = null!; // must come from auth

            ToDoItem item = toDoItemDto.ToToDoItem();

            item.CreatedByUserId = user.Id;
            item.UpdatedByUserId = user.Id;

            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;

            await _todoRepository.AddAsync(item);
            await _todoRepository.SaveChangesAsync();

            return item.ToToDoItemResponseDto();
        }

        public async Task UpdateAsync(UpdateToDoItemDto dto)
        {
            User user = null!;// comes from auth

            var item = await _todoRepository.GetByIdAsync(dto.Id);
            if (item == null)
                throw new EntityNotFoundException(nameof(ToDoItem), dto.Id);

            item.Title = dto.Title;

            item.Description = dto.Description;

            item.DueDate = dto.DueDate;

            item.PriorityLevel = dto.PriorityLevel;

            item.UpdatedAt = DateTime.Now;
            item.UpdatedByUserId = user.Id;

            await _todoRepository.UpdateAsync(item);
            await _todoRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            if (item == null)
                throw new EntityNotFoundException(nameof(ToDoItem), id);

            await _todoRepository.DeleteAsync(item);
            await _todoRepository.SaveChangesAsync();
        }
    }
}

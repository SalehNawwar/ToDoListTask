using Application.Common;
using Application.DTOs.ToDoItemDtos;
using Application.DTOs.UserDtos;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mapping;
using Application.Validators;
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

        public ToDoItemService(IToDoItemRepository todoRepository)
        {
            _todoRepository = todoRepository;
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

        public async Task<ToDoItemResponseDto> CreateAsync(long userId,CreateToDoItemDto toDoItemDto)
        {
            Validator.Validate(new CreateToDoItemDtoValidator(), toDoItemDto);

            ToDoItem item = toDoItemDto.ToToDoItem();
            
            item.CreatedByUserId = userId;
            item.UpdatedByUserId = userId;

            item.CreatedAt = DateTime.Now;
            item.UpdatedAt = DateTime.Now;

            await _todoRepository.AddAsync(item);
            await _todoRepository.SaveChangesAsync();

            return item.ToToDoItemResponseDto();
        }

        
        public async Task UpdateAsync(long userId,UpdateToDoItemDto dto)
        {
            Validator.Validate(new UpdateToDoItemDtoValidator(), dto);

            var item = await _todoRepository.GetByIdAsync(dto.Id);
            if (item == null)
                throw new EntityNotFoundException(nameof(ToDoItem), dto.Id);

            if(string.IsNullOrEmpty(dto.Title)==false)
                item.Title = dto.Title;

            if(string.IsNullOrEmpty(dto.Description)==false)
                item.Description = dto.Description;

            if(dto.DueDate.HasValue)
                item.DueDate = dto.DueDate.Value;

            if(dto.PriorityLevel.HasValue)
                item.PriorityLevel = dto.PriorityLevel.Value;

            if (dto.IsCompleted.HasValue)
                item.IsCompleted = dto.IsCompleted.Value;

            if (dto.AssignedToUserId.HasValue)
                item.AssignedToUserId = dto.AssignedToUserId.Value;

            item.UpdatedAt = DateTime.Now;
            item.UpdatedByUserId = userId;

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

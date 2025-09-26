using Application.DTOs.ToDoItemDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public static class ToDoItemMapper
    {
        public static ToDoItem ToToDoItem(this CreateToDoItemDto createToDoItemDto)
        {
            return new ToDoItem()
            {
                Title = createToDoItemDto.Title,
                Description = createToDoItemDto.Description,
                PriorityLevel = createToDoItemDto.PriorityLevel,
                DueDate = createToDoItemDto.DueDate,
                IsCompleted = createToDoItemDto.IsCompleted,
                AssignedToUserId = createToDoItemDto.AssignedToUserId
            };
        }

        public static ToDoItemResponseDto ToToDoItemResponseDto(this ToDoItem item)
        {
            return new ToDoItemResponseDto()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                PriorityLevel = item.PriorityLevel,
                DueDate = item.DueDate,
                IsCompleted = item.IsCompleted,
                AssignedToUserId = item.AssignedToUserId
            };
        }
        public static ListingToDoItemResponseDto ToListingToDoItemResponseDto(this ToDoItem item)
        {
            return new ListingToDoItemResponseDto()
            {
                Id = item.Id,
                Title = item.Title,
                PriorityLevel = item.PriorityLevel,
                DueDate = item.DueDate,
                IsCompleted = item.IsCompleted,
                AssignedToUserId = item.AssignedToUserId
            };
        }
    }
}

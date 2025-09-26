using Application.Common;
using Application.DTOs.ToDoItemDtos;
using Domain.Entities;
namespace Application.Interfaces.Services
{
    public interface IToDoItemService
    {
        Task<ToDoItemResponseDto?> GetByIdAsync(long id);

        Task<PagedResult<ListingToDoItemResponseDto>> GetAllAsync(ToDoItemRequestParameters parameters);

        Task<ToDoItemResponseDto> CreateAsync(long userId, CreateToDoItemDto dto);

        Task UpdateAsync(long userId, UpdateToDoItemDto dto);

        Task DeleteAsync(long id);
    }

}
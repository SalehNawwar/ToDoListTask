// Application/Interfaces/IToDoItemRepository.cs
using Domain.Entities;
using Application.Common;

namespace Application.Interfaces.Repositories
{
    public interface IToDoItemRepository
    {
        Task<ToDoItem?> GetByIdAsync(long id);

        IQueryable<ToDoItem> Query();
        Task<PagedResult<ToDoItem>> GetAllAsync(ToDoItemRequestParameters parameters);

        Task AddAsync(ToDoItem item);
        Task UpdateAsync(ToDoItem item);
        Task DeleteAsync(ToDoItem item);
        Task<int> SaveChangesAsync();
    }
}

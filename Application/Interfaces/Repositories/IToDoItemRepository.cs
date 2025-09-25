// Application/Interfaces/IToDoItemRepository.cs
using Domain.Entities;
using Application.Common;

namespace Application.Interfaces.Repositories
{
    public interface IToDoItemRepository
    {
        Task<ToDoItem?> GetByIdAsync(long id);

        IQueryable<ToDoItem> Query();
        Task<PagedResult<ToDoItem>> GetAllAsync(int pageNumber, int pageSize);

        Task<PagedResult<ToDoItem>> GetByCreatorIdAsync(long userId, int pageNumber, int pageSize);
        Task<PagedResult<ToDoItem>> GetByAssignedIdAsync(long userId, int pageNumber, int pageSize);
        Task<PagedResult<ToDoItem>> SearchByTitleAsync(string title, int pageNumber, int pageSize);

        Task AddAsync(ToDoItem item);
        Task UpdateAsync(ToDoItem item);
        Task DeleteAsync(ToDoItem item);
        Task<int> SaveChangesAsync();
    }
}

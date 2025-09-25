// Application/Interfaces/IUserRepository.cs
using Domain.Entities;
using Application.Common;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Query();
        Task<User?> GetByIdAsync(long id);
        Task<User?> GetByUserNameAsync(string userName);

        Task<PagedResult<User>> GetAllAsync(int pageNumber, int pageSize);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
    }
}

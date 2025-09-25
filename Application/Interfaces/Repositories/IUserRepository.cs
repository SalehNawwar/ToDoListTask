// Application/Interfaces/IUserRepository.cs
using Domain.Entities;
using Application.Common;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Query();
        Task<User?> GetByIdAsync(long id);
        Task<PagedResult<User>> GetAllAsync(UserRequestParameters parameters);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
    }
}

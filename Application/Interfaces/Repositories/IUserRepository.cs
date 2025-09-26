// Application/Interfaces/IUserRepository.cs
using Domain.Entities;
using Application.Common;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUserNameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string username);
        IQueryable<User> Query();
        Task<User?> GetByIdAsync(long id);
        Task<PagedResult<User>> GetAllAsync(UserRequestParameters parameters);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
    }
}

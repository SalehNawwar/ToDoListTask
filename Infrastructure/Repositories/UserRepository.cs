using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoListDBContext _db;

        public UserRepository(ToDoListDBContext db)
        {
            _db = db;
        }

        public IQueryable<User> Query()
        {
            return _db.Users.AsQueryable();
        }
        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
            return Task.CompletedTask;
        }

        public async Task<PagedResult<User>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _db.Users.CountAsync();
            var items = await _db.Users
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

            return new PagedResult<User>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            return Task.CompletedTask;
        }
    }
}

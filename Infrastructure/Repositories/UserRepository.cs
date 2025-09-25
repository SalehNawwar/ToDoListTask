using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

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

        public async Task<PagedResult<User>> GetAllAsync(UserRequestParameters parameters)
        {
            var query = _db.Users.AsQueryable();
            
            if(parameters.InEmail.IsNullOrEmpty() == false)
            {
                query = query.Where(user => user.UserEmail.Contains(parameters.InEmail));
            }

            if (parameters.InUserName.IsNullOrEmpty() == false)
            {
                query = query.Where(user => user.UserEmail.Contains(parameters.InUserName));
            }

            if (parameters.Role.HasValue)
            {
                query = query.Where(user => user.Role==parameters.Role.Value);
            }

            if (parameters.SortedByNameAscending.HasValue 
                && parameters.SortedByNameAscending.Value == true)
            {
                query = query.OrderBy(user => user.UserName);
            }

            if (parameters.SortedByNameAscending.HasValue
                && parameters.SortedByNameAscending.Value == false)
            {
                query = query.OrderByDescending(user => user.UserName);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                                 .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                 .Take(parameters.PageSize)
                                 .ToListAsync();

            return new PagedResult<User>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
            };
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(user => user.Id == id);
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

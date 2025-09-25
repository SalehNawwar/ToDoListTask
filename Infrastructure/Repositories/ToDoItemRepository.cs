using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Infrastructure.Repositories
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoListDBContext _db;

        public ToDoItemRepository(ToDoListDBContext db)
        {
            _db = db;
        }
        public async Task AddAsync(ToDoItem item)
        {
            await _db.ToDoItems
                .AddAsync(item);
        }

        public Task DeleteAsync(ToDoItem item)
        {
            _db.ToDoItems
                .Remove(item);
            return Task.CompletedTask;
        }

        public async Task<PagedResult<ToDoItem>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _db.ToDoItems.CountAsync();
            var items = await _db.ToDoItems
                                 .Skip((pageNumber - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

            return new PagedResult<ToDoItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<ToDoItem>> GetByAssignedIdAsync(long userId, int pageNumber, int pageSize)
        {
            var query = _db.ToDoItems.Where(t => t.AssignedToUserId == userId);

            var totalCount = await query.CountAsync();
            
            var items = await query
                     .Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .ToListAsync();

            return new PagedResult<ToDoItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PagedResult<ToDoItem>> GetByCreatorIdAsync(long userId, int pageNumber, int pageSize)
        {
            var query = _db.ToDoItems.Where(t => t.CreatedByUserId == userId);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PagedResult<ToDoItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ToDoItem?> GetByIdAsync(long id)
        {
            return await _db.ToDoItems
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public IQueryable<ToDoItem> Query()
        {
            return _db.ToDoItems.AsQueryable();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<PagedResult<ToDoItem>> SearchByTitleAsync(string title, int pageNumber, int pageSize)
        {
            var query = _db.ToDoItems.Where(t => 
                t.Title
                .Trim()
                .ToLower()
                .Contains(title.Trim().ToLower()
            ));

            var totalCount = await query.CountAsync();

            var items = await _db.ToDoItems
                     .Skip((pageNumber - 1) * pageSize)
                     .Take(pageSize)
                     .ToListAsync();
            return new PagedResult<ToDoItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public Task UpdateAsync(ToDoItem item)
        {
            _db.ToDoItems
                .Update(item);
            return Task.CompletedTask;
        }
    }
}

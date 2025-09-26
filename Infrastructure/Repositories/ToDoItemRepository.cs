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
using Microsoft.IdentityModel.Tokens;

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

        public async Task<PagedResult<ToDoItem>> GetAllAsync(ToDoItemRequestParameters parameters)
        {
            var query = _db.ToDoItems.AsQueryable();

            if (parameters.UserAssignedId.HasValue)
            {
                query = query.Where(item => item.AssignedToUserId == parameters.UserAssignedId.Value);
            }

            if (parameters.IsCompleted.HasValue)
            {
                query = query.Where(item => item.IsCompleted == parameters.IsCompleted.Value);
            }

            if (parameters.PriorityLevel.HasValue)
            {
                query = query.Where(item => item.PriorityLevel == parameters.PriorityLevel.Value);
            }

            if (parameters.From.HasValue)
            {
                query = query.Where(item => item.DueDate >= parameters.From.Value);
            }

            if (parameters.To.HasValue)
            {
                query = query.Where(item => item.DueDate <= parameters.To.Value);
            }

            if (string.IsNullOrEmpty(parameters.Title)==false)
            {
                query = query.Where(item => item.Title.Contains(parameters.Title));
            }

            if (string.IsNullOrEmpty(parameters.Description) == false)
            {
                query = query.Where(item => string.IsNullOrEmpty(item.Description) ? false : item.Description.Contains(parameters.Description));
            }

            switch (parameters.SortingParameter)
            {
                case SortingParameters.None:
                    break;

                case SortingParameters.ByTitleAscending:
                    query = query.OrderBy(item => item.Title);
                    break;

                case SortingParameters.ByTitleDescending:
                    query = query.OrderByDescending(item => item.Title);
                    break;

                case SortingParameters.ByPriorityLevelAscending:
                    query = query.OrderBy(item => item.PriorityLevel);
                    break;

                case SortingParameters.ByPriorityLevelDescending:
                    query = query.OrderByDescending(item => item.PriorityLevel);
                    break;
                case SortingParameters.ByDueDateAscending:
                    query = query.OrderBy(item => item.DueDate);
                    break;
                case SortingParameters.ByDueDateDescending:
                    query = query.OrderByDescending(item=>item.DueDate);
                    break;
            }

            var totalCount = await query.CountAsync();

            var items = await query
                                 .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                 .Take(parameters.PageSize)
                                 .ToListAsync();

            return new PagedResult<ToDoItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize
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

        public Task UpdateAsync(ToDoItem item)
        {
            _db.ToDoItems
                .Update(item);
            return Task.CompletedTask;
        }
    }
}

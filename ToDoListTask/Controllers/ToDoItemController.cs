using Application.Common;
using Application.DTOs.ToDoItemDtos;
using Application.DTOs.UserDtos;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;
using ToDoListTask.Helpers;

namespace ToDoListTask.Controllers
{
    /// <summary>
    /// Manages to-do items for authenticated users. it requirs logged users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoItemService _todoItemService;
        private readonly IUserService _userService;
        public ToDoItemsController(IToDoItemService todoItemService,IUserService userService)
        {
            _todoItemService = todoItemService;
            _userService = userService;
        }

        /// <summary>
        /// Gets a to-do item by ID. requires the Owner role or the task should be assigned to the current user id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItemResponseDto>> GetById(long id)
        {
            var dto = await UserSigned.Dto(User,_userService);
            var item = await _todoItemService.GetByIdAsync(id);

            if (dto?.Role != Roles.Owner && dto?.Id != item?.AssignedToUserId)
                throw new Application.Exceptions.UnauthorizedActionException("not allowed");

            return Ok(item);
        }

        /// <summary>
        /// Gets all to-do items for the current user.
        /// if the current user is Owner then it gets every thing
        /// if not it gets only the tasks assigned to the user
        /// it supports filting and sorting
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<ToDoItemResponseDto>>> GetAll([FromQuery] ToDoItemRequestParameters parameters)
        {
            var dto = await UserSigned.Dto(User, _userService);

            if (dto?.Role != Roles.Owner)
                parameters.UserAssignedId = dto?.Id;
            
            var items = await _todoItemService.GetAllAsync(parameters);
            return Ok(items);
        }

        /// <summary>
        /// Creates a new to-do item.
        /// requires Owner role
        /// </summary>
        [HttpPost]
        [Authorize(Roles="Owner")]
        public async Task<ActionResult<ToDoItemResponseDto>> Create([FromBody] CreateToDoItemDto dto)
        {
            long userId = 0;
            long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,out userId);

            var item = await _todoItemService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates an existing to-do item.
        /// requires Owner role
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles="Owner")]
        public async Task<IActionResult> Update(long id,[FromBody] UpdateToDoItemDto dto)
        {
            long userId = 0;
            long.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);

            dto.Id = id;
            await _todoItemService.UpdateAsync(userId,dto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a to-do item.
        /// requires Owner role
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles="Owner")]
        public async Task<IActionResult> Delete(long id)
        {
            await _todoItemService.DeleteAsync(id);
            return NoContent();
        }
    }
}

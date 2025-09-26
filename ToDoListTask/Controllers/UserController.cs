using Application.Common;
using Application.DTOs.UserDtos;
using Application.Interfaces.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ToDoListTask.Helpers;

namespace ToDoListTask.Controllers
{
    /// <summary>
    /// Manages user accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        /// <summary>
        /// Gets a user by ID. if the caller not Owner role then this will fail
        /// if the user asks for himself it works without admin role
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetById(long id)
        {
            var dto = await UserSigned.Dto(User, _userService);

            if (dto.Role != Roles.Owner && dto.Id != id)
                throw new Application.Exceptions.UnauthorizedActionException("not allowed");

            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Gets all users with pagination and filtering. requires Owner role
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<PagedResult<UserResponseDto>>> GetAll([FromQuery] UserRequestParameters parameters)
        {
            var users = await _userService.GetAllAsync(parameters);
            return Ok(users);
        }


        //[HttpPost]
        //[Authorize(Roles = "Owner")]
        //public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserDto dto)
        //{
        //    var user = await _userService.CreateAsync(dto);
        //    return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        //}

        /// <summary>
        /// Updates an existing user. requires Owner role
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateUserDto dto)
        {
            dto.Id = id;
            await _userService.UpdateAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user by ID. requires Owner role
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(long id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}

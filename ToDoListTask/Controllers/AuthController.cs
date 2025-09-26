using Application.DTOs.Auth;
using Application.Interfaces.Services;
using Application.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoListTask.Controllers
{
    /// <summary>
    /// Authentication endpoints for registering and logging in users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user [requires Owner role].
        /// the owner account should use this endpoint to create new user to handle password hashing
        /// </summary>
        /// <param name="dto">Registration details including username, email, and password.</param>
        /// <returns>Status 200 if successful.</returns>
        [HttpPost("register")]
        [Authorize(Roles ="Owner")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok("User registered successfully");
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="dto">Login credentials (username and password).</param>
        /// <returns>JWT token if authentication succeeds.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.AuthenticateAsync(dto);
            return Ok(new { Token = token });
        }
    }
}

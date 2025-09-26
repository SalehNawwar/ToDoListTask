using Application.DTOs.UserDtos;
using Domain.Entities;
using System.Security.Claims;
using Microsoft.Identity.Client;

using Microsoft.AspNetCore.Authorization;
using Application.Interfaces.Services;

namespace ToDoListTask.Helpers
{
    public static class UserSigned
    {
        public static async Task<UserResponseDto?> Dto(ClaimsPrincipal user,IUserService _userService)
        {
            long userId = 0;
            long.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out userId);
            return await _userService.GetByIdAsync(userId);
        }
    }
}

using Domain.Entities;
using Application.DTOs.Auth;

namespace Application.Interfaces.Services
{

    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(LoginDto loginDto);
        Task RegisterAsync(RegisterDto dto);

        Task<string?> GetUserIdFromTokenAsync(string token);
        Task<string?> GetUserRoleFromTokenAsync(string token);

        Task<bool> AuthorizeAsync(string token, string requiredRole);
    }

}
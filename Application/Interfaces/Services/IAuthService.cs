using Domain.Entities;

namespace Application.Interfaces.Services
{

    public interface IAuthService
    {
        Task<string?> AuthenticateAsync(string username, string password);

        Task RegisterAsync(string username, string password, string role);

        Task<User?> GetUserFromTokenAsync(string token);
        Task<string?> GetUserIdFromTokenAsync(string token);
        Task<string?> GetUserRoleFromTokenAsync(string token);

        Task<bool> AuthorizeAsync(string token, string requiredRole);
        Task<bool> IsOwnerAsync(string token, string resourceOwnerId);
        Task<string> RefreshTokenAsync(string refreshToken);
    }

}
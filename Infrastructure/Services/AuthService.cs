using Application.DTOs.Auth;
using Application.DTOs.UserDtos;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<string?> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUserNameAsync(loginDto.UserName);
            if (user == null || !_passwordHasher.Verify(loginDto.Password, user.UserPassword))
                throw new InvalidCredentialsException();

            return GenerateJwtToken(user);
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
                throw new DuplicateEmailException(dto.Email);

            if (await _userRepository.ExistsByUserNameAsync(dto.UserName))
                throw new DuplicateUserNameException(dto.UserName);

            Roles role;

            if(Roles.TryParse(dto.Role,out role) == false)
                throw new InvalidRoleException(dto.Role);
            
            var user = new User
            {
                UserName = dto.UserName,
                UserEmail = dto.Email,
                Role = role,
                UserPassword = _passwordHasher.Hash(dto.Password)
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public Task<string?> GetUserIdFromTokenAsync(string token)
        {
            var principal = GetPrincipalFromToken(token);
            var userId = principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Task.FromResult(userId);
        }

        public Task<string?> GetUserRoleFromTokenAsync(string token)
        {
            var principal = GetPrincipalFromToken(token);
            var role = principal?.FindFirst(ClaimTypes.Role)?.Value;
            return Task.FromResult(role);
        }

        public async Task<bool> AuthorizeAsync(string token, string requiredRole)
        {
            var role = await GetUserRoleFromTokenAsync(token);
            return role != null && role.Equals(requiredRole, StringComparison.OrdinalIgnoreCase);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, nameof(user.Role))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}

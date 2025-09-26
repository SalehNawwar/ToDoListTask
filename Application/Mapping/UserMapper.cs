
using Application.DTOs.Auth;
using Application.DTOs.UserDtos;
using Domain.Entities;
using System.Runtime.CompilerServices;

namespace Application.Mapping
{
    public static class UserMapper
    {
        public static UserResponseDto ToUserResponseDto(this User user)
        {
            return new UserResponseDto()
            {
                Id = user.Id,
                UserEmail = user.UserEmail,
                UserName = user.UserName,
                Role = user.Role,
            };
        }
        public static User ToUser(this CreateUserDto user)
        {
            return new User()
            {
                UserEmail = user.UserEmail,
                UserName = user.UserName,
                Role = user.Role,
                UserPassword = user.UserPassword,
            };
        }

    }
}

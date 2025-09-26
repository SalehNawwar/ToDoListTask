
using Domain.Enums;

namespace Application.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public Roles? Role { get; set; }

    }
}

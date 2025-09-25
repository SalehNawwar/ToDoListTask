
using Domain.Enums;

namespace Application.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public Roles Role { get; set; }

    }
}

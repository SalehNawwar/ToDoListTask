
using Domain.Enums;

namespace Application.DTOs.UserDtos
{
    public class CreateUserDto
    {

        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public Roles Role {  get; set; }
    }
}

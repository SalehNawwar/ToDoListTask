
using Domain.Enums;

namespace Application.Common
{
    public class UserRequestParameters : RequestParameters
    {
        public string InUserName { get; set; } = string.Empty;
        public string InEmail { get; set; } = string.Empty;
        public Roles? Role { get; set; }

        public bool? SortedByNameAscending { get; set; }
    }
}

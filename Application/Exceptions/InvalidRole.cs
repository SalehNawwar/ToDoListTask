using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InvalidRoleException:Exception
    {
        public InvalidRoleException(string roleName)
            : base($"The role '{roleName}' is invalid.") { }
    }
}

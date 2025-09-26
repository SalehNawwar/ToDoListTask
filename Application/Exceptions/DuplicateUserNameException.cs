using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DuplicateUserNameException : Exception
    {
        public DuplicateUserNameException(string userName)
            : base($"The username '{userName}' is already in use.") { }
    }
}

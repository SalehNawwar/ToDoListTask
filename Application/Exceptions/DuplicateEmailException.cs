﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DuplicateEmailException:Exception
    {
        public DuplicateEmailException(string email)
            : base($"The email '{email}' is already in use.") { }
    }
}

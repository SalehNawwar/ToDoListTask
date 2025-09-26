﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EntityNotFoundException:Exception
    {
        public EntityNotFoundException(string entity, object key)
            : base($"{entity} with key '{key}' was not found.") { }
    }
}

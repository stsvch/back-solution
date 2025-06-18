﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(Guid id)
            : base($"Entity with Id = '{id}' was not found.") { }

        public EntityNotFoundException() : base($"Entity was not found.") { }
    }
}

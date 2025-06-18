﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Domain.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}

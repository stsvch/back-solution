﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.DTOs
{
    public record PhotoDto(
        Guid Id,
        string Path
    );
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleSystem.DTO;

namespace SaleSystem.BLL.Services.Interfaces
{
    public interface IRolService
    {
        Task<List<RolDTO>> List();

    }
}

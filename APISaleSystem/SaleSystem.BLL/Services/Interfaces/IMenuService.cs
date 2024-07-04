using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleSystem.DTO;

namespace SaleSystem.BLL.Services.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuDTO>> List(int userId);
    }
}

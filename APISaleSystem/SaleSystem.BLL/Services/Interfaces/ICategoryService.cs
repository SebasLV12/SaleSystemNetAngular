using SaleSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> List();
    }
}

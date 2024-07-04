using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleSystem.DTO;

namespace SaleSystem.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> List();
        Task<ProductDTO> Create(ProductDTO model);
        Task<bool> Update(ProductDTO model);
        Task<bool> Delete(int id);
    }
}

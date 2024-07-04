using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SaleSystem.Model;

namespace SaleSystem.DAL.Repositories.Interfaces
{
    public interface ISaleRepository: IGenericRepository<Sale>
    {

        Task<Sale> Register(Sale model);
    }
}

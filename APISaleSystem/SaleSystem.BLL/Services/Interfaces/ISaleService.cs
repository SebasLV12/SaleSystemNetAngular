using SaleSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.BLL.Services.Interfaces
{
    public interface ISaleService
    {
        Task<SaleDTO> Register(SaleDTO model);
        Task<List<SaleDTO>> History(string findBy,string saleNumber,string startDate, string dueDate);
        Task<List<ReportDTO>> Report(string startDate, string dueDate);
    }
}

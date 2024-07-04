using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class DashboardDTO
    {
        public int TotalSale { get; set; }
        public string? TotalIncome { get; set; }
        public int TotalProducts {  get; set; }
        public List<WeekSaleDTO> SaleLastWeek { get; set; }
    }
}

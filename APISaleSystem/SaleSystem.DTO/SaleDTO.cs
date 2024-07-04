using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class SaleDTO
    {
        public int IdSale { get; set; }
        public string? NumberDoc { get; set; }
        public string? PaymentType { get; set; }
        public string? TotalString { get; set; }
        public string? CreatedOn { get; set; }
        public virtual ICollection<DetailSaleDTO> DetailSales { get; set; }
    }
}

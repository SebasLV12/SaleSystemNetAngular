using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class DetailSaleDTO
    {
        public int? IdProduct { get; set; }
        public int? ProductDescription { get; set; }
        public int? Amount { get; set; }

        public string? PriceString { get; set; }

        public string? TotalString { get; set; }
    }
}

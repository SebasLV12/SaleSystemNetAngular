using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class ReportDTO
    {
        public string? NumberDocument {  get; set; }
        public string? PaymentType { get; set; }
        public string? CreatedOn { get; set; }
        public string? TotalSale { get; set; }
        public string? Product {  get; set; }
        public string? Amount { get; set; }
        public string? Price { get; set; }
        public string? Total {  get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class Sale
{
    public int IdSale { get; set; }

    public string? NumberDoc { get; set; }

    public string? PaymentType { get; set; }

    public decimal? Total { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<DetailSale> DetailSales { get; } = new List<DetailSale>();
}

using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? Name { get; set; }

    public int? IdCategory { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<DetailSale> DetailSales { get; } = new List<DetailSale>();

    public virtual Category? IdCategoryNavigation { get; set; }
}

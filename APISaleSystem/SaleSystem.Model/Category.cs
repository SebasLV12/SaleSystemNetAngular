using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class Category
{
    public int IdCategory { get; set; }

    public string? Name { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}

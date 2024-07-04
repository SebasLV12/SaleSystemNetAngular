using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class NumberDocument
{
    public int IdNumberDoc { get; set; }

    public int LastNumber { get; set; }

    public DateTime? CreatedOn { get; set; }
}

using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class User
{
    public int IdUsuario { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public int? IdRol { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}

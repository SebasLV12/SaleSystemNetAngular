﻿using System;
using System.Collections.Generic;

namespace SaleSystem.Model;

public partial class Rol
{
    public int IdRol { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedOn { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; } = new List<MenuRol>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}

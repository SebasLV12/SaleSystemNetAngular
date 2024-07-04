using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleSystem.DTO
{
    public class SessionDTO
    {
        public int IdUsuario { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? RolDescription { get; set; }
    }

}

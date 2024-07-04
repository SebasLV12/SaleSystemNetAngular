using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SaleSystem.DTO;

namespace SaleSystem.BLL.Services.Interfaces
{
    public interface IUserService
    {

        Task<List<UserDTO>> List();
        Task<SessionDTO> ValidateUser(string email, string password);
        Task<UserDTO> Create(UserDTO model);
        Task<bool> Update(UserDTO model);
        Task<bool> Delete(int id);
    }
}

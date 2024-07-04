using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.DAL.Repositories.Interfaces;
using SaleSystem.DTO;
using SaleSystem.Model;


namespace SaleSystem.BLL.Services
{
    public class MenuService:IMenuService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<User> userRepository, IGenericRepository<MenuRol> menuRolRepository, IGenericRepository<Menu> menuRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _menuRolRepository = menuRolRepository;
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> List(int userId)
        {
            IQueryable<User> tbUser=await _userRepository.DbQuery(u=>u.IdUsuario==userId);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepository.DbQuery();
            IQueryable<Menu> tbMenu = await _menuRepository.DbQuery();

            try
            {
                IQueryable<Menu> tbResponse=(from u in tbUser
                                             join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                             join m in tbMenu on mr.IdMenu equals m.IdMenu
                                             select m).AsQueryable();
                var listOfMenus=tbResponse.ToList();
                return _mapper.Map<List<MenuDTO>>(listOfMenus);

            }catch{
                throw;
            }
        }
    }
}

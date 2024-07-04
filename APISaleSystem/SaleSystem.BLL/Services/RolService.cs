﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.DAL.Repositories.Interfaces;
using SaleSystem.DTO;
using SaleSystem.Model;


namespace SaleSystem.BLL.Services
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> List()
        {
            try
            {
                var listOfRoles = await _rolRepository.DbQuery();
                return _mapper.Map<List<RolDTO>>(listOfRoles.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}

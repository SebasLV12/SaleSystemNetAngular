using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.DAL.Repositories.Interfaces;
using SaleSystem.DTO;
using SaleSystem.Model;

namespace SaleSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> List()
        {
            try
            {
                var userQuery = await _userRepository.DbQuery();
                var listOfUsers = userQuery.Include(rol => rol.IdRolNavigation).ToList();

                return _mapper.Map<List<UserDTO>>(listOfUsers);
            }
            catch
            {
                throw;
            }
        }
        public async Task<SessionDTO> ValidateUser(string email, string password)
        {
            try
            {
                var userQuery = await _userRepository.DbQuery(u =>
                u.Email == email &&
                u.Password == password);

                if (userQuery.FirstOrDefault() == null)
                    throw new Exception("The user does not exist");

                User returnUser = userQuery.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<SessionDTO>(returnUser);


            }
            catch
            {
                throw;
            }
        }
        public async Task<UserDTO> Create(UserDTO model)
        {
            try
            {
                var newUser = await _userRepository.Create(_mapper.Map<User>(model));

                if (newUser.IdUsuario == 0)
                    throw new TaskCanceledException("Can not create");

                var query = await _userRepository.DbQuery(u => u.IdUsuario == newUser.IdUsuario);

                newUser = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UserDTO>(newUser);
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Update(UserDTO model)
        {
            try
            {
                var userModel = _mapper.Map<User>(model);
                var userFounded = await _userRepository.Get(u => u.IdUsuario == userModel.IdUsuario);

                if (userFounded == null)
                    throw new TaskCanceledException("User does not exist");

                userFounded.FullName = userModel.FullName;
                userFounded.Email = userModel.Email;
                userFounded.IdRol = userModel.IdRol;
                userFounded.Password = userFounded.Password;
                userFounded.IsActive = userModel.IsActive;

                bool response = await _userRepository.Update(userFounded);

                if (!response)
                    throw new TaskCanceledException("Can not update");
                return response;

            }
            catch
            {
                throw;
            }
        }



        public async Task<bool> Delete(int id)
        {
            try
            {
                var userFounded = await _userRepository.Get(u => u.IdUsuario == id);
                if (userFounded == null)
                    throw new TaskCanceledException("The user does not exist");

                bool response = await _userRepository.Delete(userFounded);
                if (!response)
                    throw new TaskCanceledException("Can not delete");
                return response;

            }
            catch
            {
                throw;
            }
        }

    }

}

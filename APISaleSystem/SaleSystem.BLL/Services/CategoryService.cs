using System;
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
    public class CategoryService:ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> List()
        {
            try
            {
                var listOfCategories = await _categoryRepository.DbQuery();
                return _mapper.Map<List<CategoryDTO>>(listOfCategories.ToList());

            }
            catch
            {
                throw;
            }
        }
    }
}

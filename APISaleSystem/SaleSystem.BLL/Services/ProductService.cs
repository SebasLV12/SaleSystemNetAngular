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
    public class ProductService:IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> List()
        {
            try
            {
                var productQuery = await _productRepository.DbQuery();
                var listOfProduct=productQuery.Include(cat=>cat.IdCategoryNavigation).ToList();

                return _mapper.Map<List<ProductDTO>>(listOfProduct);

            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductDTO> Create(ProductDTO model)
        {
            try
            {
                var newProduct= await _productRepository.Create(_mapper.Map<Product>(model));

                if (newProduct.IdProduct == 0)
                    throw new TaskCanceledException("Can not create");

                return _mapper.Map<ProductDTO>(newProduct);
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Update(ProductDTO model)
        {
            try{
                var productModel=_mapper.Map<Product>(model);
                var productFounded=await _productRepository.Get(p=>p.IdProduct==productModel.IdProduct);

                if (productFounded == null)
                    throw new TaskCanceledException("The product does not exist");

                productFounded.Name= productModel.Name;
                productFounded.IdCategory = productModel.IdCategory;
                productFounded.Stock = productModel.Stock;
                productFounded.Price=productModel.Price;
                productFounded.IsActive=productModel.IsActive;

                bool response=await _productRepository.Update(productFounded);

                if (response)
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
                var productFounded = await _productRepository.Get(p => p.IdProduct == id);

                if (productFounded == null)
                    throw new TaskCanceledException("The product does not exist");

                bool response = await _productRepository.Delete(productFounded);

                if (response)
                    throw new TaskCanceledException("Can not update");

                return response;
            }
            catch
            {
                throw;
            }
            

        }

      
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SaleSystem.DAL.Repositories.Interfaces;
using SaleSystem.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SaleSystem.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class 
    {
        private readonly DbsalesContext _dbContext;

        public GenericRepository(DbsalesContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;

            }catch {
                throw;
            }
        }
        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch 
            {
                throw;
            }
        }
    

        public async Task<bool> Update(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }catch 
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
             try
            {
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }catch 
            {
                throw;
             }
        }


        public async Task<IQueryable<TModel>> DbQuery(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {

                IQueryable<TModel> queryModel = filter == null ?  _dbContext.Set<TModel>(): _dbContext.Set<TModel>().Where(filter);
                return queryModel;

            }
            catch 
            {
                throw;
            }

        }

    }
    
}

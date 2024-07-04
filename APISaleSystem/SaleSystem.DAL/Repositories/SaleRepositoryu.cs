using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SaleSystem.DAL.DBContext;
using SaleSystem.DAL.Repositories.Interfaces;
using SaleSystem.Model;

namespace SaleSystem.DAL.Repositories
{
    public class SaleRepository:GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsalesContext _dbContext;

        public SaleRepository(DbsalesContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Sale> Register(Sale model)
        {
            Sale newSale = new Sale();

            using(var trasaction=_dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach(DetailSale dv in model.DetailSales)
                    {
                        Product productFounded = _dbContext.Products.Where(p => p.IdProduct == dv.IdProduct).First();
                        productFounded.Stock=productFounded.Stock-dv.Amount;
                        _dbContext.Products.Update(productFounded);
                    }
                    await _dbContext.SaveChangesAsync();

                    NumberDocument correlative = _dbContext.NumberDocuments.First();

                    correlative.LastNumber = correlative.LastNumber + 1;
                    correlative.CreatedOn = DateTime.Now;

                    _dbContext.NumberDocuments.Update(correlative);
                    await _dbContext.SaveChangesAsync();

                    int lenghtDigit = 4;
                    string ceros = string.Concat(Enumerable.Repeat("O", lenghtDigit));
                    string saleNumber = ceros + correlative.LastNumber.ToString();

                    saleNumber=saleNumber.Substring(saleNumber.Length-lenghtDigit);

                    model.NumberDoc = saleNumber;  

                    await _dbContext.Sales.AddAsync(model);
                    await _dbContext.SaveChangesAsync();

                    newSale=model;

                    trasaction.Commit();

                }
                catch
                {
                    trasaction.Rollback();
                    throw;

                }
                return newSale;
            }
        }
    }
}

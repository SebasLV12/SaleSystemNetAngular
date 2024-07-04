using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class DashboardService : IDashboardService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public DashboardService(ISaleRepository saleRepository, IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        private IQueryable<Sale> ReturnSales(IQueryable<Sale> saleTable,int substractDays) 
        {
            DateTime? lastDate=saleTable.OrderByDescending(v=>v.CreatedOn).Select(v=>v.CreatedOn).First();

            lastDate = lastDate.Value.AddDays(substractDays);
            
            return saleTable.Where(v=>v.CreatedOn.Value.Date>=lastDate.Value.Date); 
        }

        private async Task<int> TotalSalesLastWeek()
        {
            int total = 0;
            IQueryable<Sale> _saleQuery = await _saleRepository.DbQuery();
            if(_saleQuery.Count() > 0) 
            {
                var saleTable = ReturnSales(_saleQuery, -7);
                total=saleTable.Count();
            }
            return total;
        }

        private async Task<string> TotalIncomeLastWeek()
        {
            decimal response = 0;
            IQueryable<Sale> _saleQuery=await _saleRepository.DbQuery();    

            if(_saleQuery.Count()>0)
            {
                var saleTable= ReturnSales(_saleQuery, -7);
                response = saleTable.Select(v => v.Total).Sum(v => v.Value);
            }
            return Convert.ToString(response,new CultureInfo("es-PE"));
        }
        private async Task<int> TotalProduct()
        {
            IQueryable<Sale> _saleQuery = await _saleRepository.DbQuery();
    
            int total=_saleQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> SalesLastWeek()
        {
            Dictionary<string, int> response = new Dictionary<string, int>();

            IQueryable<Sale> _saleQuery = await _saleRepository.DbQuery();

            if (_saleQuery.Count() > 0)
            {
                var saleTable = ReturnSales(_saleQuery, -7);

                response = saleTable
                    .GroupBy(v => v.CreatedOn.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { date = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.date, elementSelector: r => r.total);
            }
            return response;
        }
        public async Task<DashboardDTO> Resume()
        {
           DashboardDTO vmDashBoard= new DashboardDTO();

            try
            {
                vmDashBoard.TotalSale = await TotalSalesLastWeek();
                vmDashBoard.TotalIncome= await TotalIncomeLastWeek();
                vmDashBoard.TotalProducts = await TotalProduct();
               
                List<WeekSaleDTO> listSalesWeek=new List<WeekSaleDTO>();

                foreach(KeyValuePair<string,int> item in await SalesLastWeek())
                {
                    listSalesWeek.Add(new WeekSaleDTO()
                    {
                        Date = item.Key,
                        Total = item.Value
                    });
                }
                vmDashBoard.SaleLastWeek = listSalesWeek;   
            } catch
            {
                throw;
            }
            return vmDashBoard;
        }
    }
}

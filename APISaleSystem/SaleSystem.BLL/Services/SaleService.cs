using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.DAL.Repositories.Interfaces;
using SaleSystem.DTO;
using SaleSystem.Model;

namespace SaleSystem.BLL.Services
{
    public class SaleService:ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IGenericRepository<DetailSale> _detailSaleRepository;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository saleRepository, IGenericRepository<DetailSale> detailSaleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _detailSaleRepository = detailSaleRepository;
            _mapper = mapper;
        }
        public async Task<SaleDTO> Register(SaleDTO model)
        {
            try
            {
                var newSale = await _saleRepository.Register(_mapper.Map<Sale>(model));

                if (newSale.IdSale == 0)
                    throw new TaskCanceledException("Can not create");
                
                return _mapper.Map<SaleDTO>(newSale);
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<SaleDTO>> History(string findBy, string saleNumber, string startDate, string endDate)
        {
            IQueryable<Sale> query = await _saleRepository.DbQuery();
            var listOfResponse=new List<Sale>();  

            try
            {
                if (findBy == "date")
                {
                    DateTime start_Date = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-PE"));
                    DateTime due_Date = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-PE"));

                    listOfResponse=await query.Where(v=>
                        v.CreatedOn.Value.Date>=start_Date.Date &&
                        v.CreatedOn.Value.Date<=due_Date.Date
                    ).Include(dv=> dv.DetailSales)
                    .ThenInclude(p => p.IdProductNavigation)
                    .ToListAsync();

                }
                else
                {
                    listOfResponse = await query.Where(v =>v.NumberDoc==saleNumber
                   ).Include(dv => dv.DetailSales)
                   .ThenInclude(p => p.IdProductNavigation)
                   .ToListAsync();
                }
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<SaleDTO>>(listOfResponse);
        }
               

        public async Task<List<ReportDTO>> Report(string startDate, string endDate)
        {

            IQueryable<DetailSale> query = await _detailSaleRepository.DbQuery();
            var listOfResponse = new List<DetailSale>();

            try
            {
                DateTime start_Date = DateTime.ParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-PE"));
                DateTime due_Date = DateTime.ParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-PE"));

                listOfResponse = await query
                    .Include(p => p.IdProduct)
                    .Include(v => v.IdSaleNavigation)
                    .Where(dv =>
                        dv.IdSaleNavigation.CreatedOn.Value.Date >= start_Date.Date &&
                        dv.IdSaleNavigation.CreatedOn.Value.Date <= due_Date.Date)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }

            return _mapper.Map<List<ReportDTO>>(listOfResponse);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.DTO;
using SaleSystem.API.Utility;
using SaleSystem.BLL.Services;

namespace SaleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] SaleDTO sale)
        {
            var rsp = new Response<SaleDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _saleService.Register(sale);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("History")]
        public async Task<IActionResult> History(string findBy, string? saleNumber,string? startDate,string? endDate)
        {
            var rsp = new Response<List<SaleDTO>>();

            saleNumber = saleNumber is null ? "" : saleNumber;
            startDate = startDate is null ? "" : startDate;
            endDate = endDate is null ? "" : endDate;

            try
            {
                rsp.Status = true;
                rsp.Value = await _saleService.History(findBy, saleNumber, startDate, endDate);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> Report(string? startDate, string? endDate)
        {
            var rsp = new Response<List<ReportDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _saleService.Report(startDate, endDate);
            }
            catch (Exception ex)
            {
                rsp.Status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}

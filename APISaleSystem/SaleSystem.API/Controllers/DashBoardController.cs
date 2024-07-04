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
    public class DashBoardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashBoardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        [Route("Resume")]
        public async Task<IActionResult> Resume()
        {
            var rsp = new Response<DashboardDTO>();
            try
            {
                rsp.Status = true;
                rsp.Value = await _dashboardService.Resume();
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

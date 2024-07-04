using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SaleSystem.BLL.Services.Interfaces;
using SaleSystem.DTO;
using SaleSystem.API.Utility;
using SaleSystem.BLL.Services.Interfaces;

namespace SaleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var rsp = new Response<List<RolDTO>>();
            try
            {
                rsp.Status = true;
                rsp.Value= await _rolService.List();
            }
            catch(Exception ex)
            {
                rsp.Status= false;
                rsp.msg=ex.Message;
            }
            return Ok(rsp);
        }
    }
}

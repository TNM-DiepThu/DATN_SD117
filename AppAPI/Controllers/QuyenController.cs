using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyenController : ControllerBase
    {
        private readonly IQuyenService _positionServiece;
        public QuyenController(IQuyenService quyenService)
        {
            _positionServiece = quyenService;
        }

        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetAllActiveAsync()
        {
            var result = await _positionServiece.GetAllPositionActive();
            return Ok(result);
        }
    }
}

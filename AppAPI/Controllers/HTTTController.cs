using AppData.Serviece.Interfaces;
using AppData.ViewModal.ThanhToanVM;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HTTTController : ControllerBase
    {
        private readonly IHinhThucThanhToanServices _httt;
        public HTTTController(IHinhThucThanhToanServices HinhThucThanhToanServiece)
        {
            _httt = HinhThucThanhToanServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var voucher = await _httt.GetAll();
            if (voucher != null) return Ok(voucher);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] HinhThucThanhToanVM p)
        {
            var result = await _httt.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _httt.Del(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] HinhThucThanhToanVM p)
        {
            var result = await _httt.Edit(id, p);
            return Ok(result);
        }
    }
}

using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamChiTietController : ControllerBase
    {
        private readonly ISanPhamChiTietServiece _sanphamCTsv;
        public SanPhamChiTietController(ISanPhamChiTietServiece sanPhamctServiece)
        {
            _sanphamCTsv = sanPhamctServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var sp = await _sanphamCTsv.GetAll();
            if (sp != null) return Ok(sp);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] SanPhamChiTietVM p)
        {
            var result = await _sanphamCTsv.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _sanphamCTsv.Del(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SanPhamChiTietVM p)
        {
            var result = await _sanphamCTsv.Edit(id, p);
            return Ok(result);
        }
    }
}

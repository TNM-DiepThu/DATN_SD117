using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamController : ControllerBase
    {
        private readonly ISanPhamServiece _sanphamsv;
        public SanphamController(ISanPhamServiece sanPhamServiece)
        {
            _sanphamsv = sanPhamServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var sp = await _sanphamsv.GetAll();
            if (sp != null) return Ok(sp);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] SanPhamvm p)
        {
            var result = await _sanphamsv.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _sanphamsv.Del(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] SanPhamvm p)
        {
            var result = await _sanphamsv.Edit(id, p);
            return Ok(result);
        }
    }
}

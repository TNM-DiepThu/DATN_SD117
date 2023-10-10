using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController : ControllerBase
    {
        private readonly IThuongHieuServiece _thuonghieusv;
        public ThuongHieuController(IThuongHieuServiece xuatSuServiece)
        {
            _thuonghieusv = xuatSuServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var thuonghieu = await _thuonghieusv.GetAll();
            if (thuonghieu != null) return Ok(thuonghieu);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] ThuongHieuVM p)
        {
            var result = await _thuonghieusv.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _thuonghieusv.Del(Id);
            return Ok(result);
        }
        
        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ThuongHieuVM p)
        {
            var result = await _thuonghieusv.Edit(id, p);
            return Ok(result);
        }
    }
}

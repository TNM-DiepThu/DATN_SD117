using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MauSacController : ControllerBase
    {
        private readonly IMauSacServiece _mausacsv;
        public MauSacController(IMauSacServiece mau)
        {
            _mausacsv = mau;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var sp = await _mausacsv.GetAll();
            if (sp != null) return Ok(sp);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] MauSacVM p)
        {
            var result = await _mausacsv.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _mausacsv.Del(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] MauSacVM p)
        {
            var result = await _mausacsv.Edit(id, p);
            return Ok(result);
        }
    }
}

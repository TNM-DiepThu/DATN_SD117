using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnhController : ControllerBase
    {
        private readonly IAnhServiece _anhsv;
        public AnhController(IAnhServiece anhServiece)
        {
            _anhsv = anhServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var sp = await _anhsv.GetAll();
            if (sp != null) return Ok(sp);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] AnhVM p)
        {
            var result = await _anhsv.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _anhsv.Del(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AnhVM p)
        {
            var result = await _anhsv.Edit(id, p);
            return Ok(result);
        }
    }
}

using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XuatSuController : ControllerBase
    {
        private readonly IXuatSuServiece _xuatsusv;
        public XuatSuController(IXuatSuServiece xuatSuServiece)
        {
            _xuatsusv = xuatSuServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var xuatsu = await _xuatsusv.GetAll();
            if (xuatsu != null) return Ok(xuatsu);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] XuatSuVM p)
        {
            var result = await _xuatsusv.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _xuatsusv.Del(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] XuatSuVM p)
        {
            var result = await _xuatsusv.Edit(id, p);
            return Ok(result);
        }
    }
}

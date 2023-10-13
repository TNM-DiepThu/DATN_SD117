using AppData.model;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly INguoiDungServiece _nguoiDungService;

        public NguoiDungController(INguoiDungServiece nguoiDungService)
        {
            _nguoiDungService = nguoiDungService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NguoiDungVM>>> GetAll()
        {
            var nguoiDungs = await _nguoiDungService.GetAll();
            return Ok(nguoiDungs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NguoiDungVM>> GetById(Guid id)
        {
            var nguoiDung = await _nguoiDungService.GetById(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return Ok(nguoiDung);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(NguoiDungVM nguoiDung)
        {
            var id = await _nguoiDungService.Create(nguoiDung);
            return CreatedAtAction("GetById", new
            {
                id
            }, nguoiDung);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, NguoiDungVM nguoiDung)
        {
            await _nguoiDungService.Update(id, nguoiDung);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _nguoiDungService.Delete(id);
            return NoContent();
        }
    }
}

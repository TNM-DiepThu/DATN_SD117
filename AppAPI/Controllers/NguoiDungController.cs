using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.Login;
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<NguoiDungVM>>> Get()
        {
            var nguoiDungs = await _nguoiDungService.GetAllAsync();
            return Ok(nguoiDungs);
        }

        [HttpGet("GetAllKH")]
        public async Task<ActionResult<IEnumerable<NguoiDungVM>>> GetAllKH()
        {
            var nguoiDungs = await _nguoiDungService.GetAllKH();
            return Ok(nguoiDungs);
        }

        [HttpGet("GetAllNV")]
        public async Task<ActionResult<IEnumerable<NguoiDungVM>>> GetAllNV()
        {
            var nguoiDungs = await _nguoiDungService.GetAllNV();
            return Ok(nguoiDungs);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<NguoiDungVM>> Get(Guid id)
        {
            var nguoiDung = await _nguoiDungService.GetByIdAsync(id);
            if (nguoiDung == null)
                return NotFound();

            return Ok(nguoiDung);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Guid>> Post([FromBody] NguoiDungVM nguoiDung)
        {
            var id = await _nguoiDungService.CreateAsync(nguoiDung);
            return CreatedAtAction(nameof(Get), new
            {
                id
            }, id);
        }

        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] NguoiDungVM nguoiDung)
        {
            await _nguoiDungService.UpdateAsync(id, nguoiDung);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _nguoiDungService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginWithJWT(LoginRequestVM loginRequest)
        {
            var result = await _nguoiDungService.LoginWithJWT(loginRequest);
            return Ok(result.Token);
        }
    }
}

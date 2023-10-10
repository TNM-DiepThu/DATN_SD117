using AppData.Serviece.Interfaces;
using AppData.ViewModal.VoucherVM;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherServices _voucher;
        public VoucherController(IVoucherServices voucherServiece)
        {
            _voucher = voucherServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var voucher = await _voucher.GetAll();
            if (voucher != null) return Ok(voucher);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] VoucherVM p)
        {
            var result = await _voucher.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _voucher.Delete(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] VoucherVM p)
        {
            var result = await _voucher.Edit(id, p);
            return Ok(result);
        }
    }
}

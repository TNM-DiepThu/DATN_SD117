using AppData.Serviece.Interfaces;
using AppData.ViewModal.VoucherVM;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherDetailController : ControllerBase
    {
        private readonly IVoucherDetailServices _voucherDetail;
        public VoucherDetailController(IVoucherDetailServices voucherDetailServiece)
        {
            _voucherDetail = voucherDetailServiece;
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAllAsync()
        {
            var voucherDetail = await _voucherDetail.GetAll();
            if (voucherDetail != null) return Ok(voucherDetail);
            return BadRequest();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] VoucherDetailVM p)
        {
            var result = await _voucherDetail.Add(p);
            return Ok(result);
        }
        [HttpDelete("Delete/{Id}")]

        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            var result = await _voucherDetail.Delete(Id);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] VoucherDetailVM p)
        {
            var result = await _voucherDetail.Edit(id, p);
            return Ok(result);
        }
    }
}

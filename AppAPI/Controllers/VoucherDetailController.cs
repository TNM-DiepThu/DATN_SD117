using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.HoaDon;
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
        private readonly VoucherViewModelService voucherViewModelService;
        public VoucherDetailController(IVoucherDetailServices voucherDetailServiece)
        {
            _voucherDetail = voucherDetailServiece;
            voucherViewModelService = new VoucherViewModelService();
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

        [HttpGet("[action]")]
        public List<VoucherDetailHoanThien> GetlistVoucherViewModelByIdNguoiDung(Guid idnguoidung)
        {
            return  voucherViewModelService.GetListVoucherViewModelByIDNguoiDung(idnguoidung);
        }
        [HttpGet("[action]")]
        public List<VoucherDetailHoanThien> GetlistVoucherViewModel()
        {
            return  voucherViewModelService.GetListVoucherViewModel();
        }
        [HttpGet("[action]")]
        public VoucherDetailHoanThien GetlistVoucherViewModelByID(Guid IdVoucherDetail)
        {
            return voucherViewModelService.GetListVoucherViewModelById(IdVoucherDetail);
        }

        [HttpGet("[action]")]
        public VoucherDetailHoanThien GetListVoucherViewModelByName(string MaVoucher)
        {
            return voucherViewModelService.GetListVoucherViewModelByName(MaVoucher);    
        }
    }
}

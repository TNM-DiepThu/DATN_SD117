using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangCTController : ControllerBase
    {
        private readonly IGioHangCTService _GH;
        private readonly ISanPhamChiTietServiece _SP;
        private readonly IGioHangService _Ga;
        public GioHangCTController()
        {
            _GH = new GioHangCTService();
        }
        [HttpGet("GetAll")]

        public IEnumerable<GioHangChiTiet> GetAllAsync()
        {
            return _GH.GetAll();
        }
        [HttpPost("Create")]
        public bool Create(int SoLuong, decimal DG, Guid IDsp, Guid Gh)
        {
            GioHangChiTiet GH = new GioHangChiTiet()
            {
                Id = Guid.NewGuid(),
                SoLuong = SoLuong,
                DonGia = DG,
                IdGioHang = _Ga.GetAll().FirstOrDefault(c => c.Id == IDsp).Id,
                IdSanPhamChiTiet = _SP.GetAll().FirstOrDefault(c => c.Id == Gh).Id,
            };
            return _GH.Add(GH);
        }
        [HttpDelete("Delete/{Id}")]

        public bool DeleteAsync(Guid Id)
        {
            var result = _GH.Del(Id);
            return result;
        }

        [HttpPut("Update/{id}")]

        public bool UpdateAsync(Guid id, [FromBody] GioHangChiTiet p)
        {
            var result = _GH.Edit(id, p);
            return result;
        }
    }
}

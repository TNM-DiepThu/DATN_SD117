using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamController : ControllerBase
    {
        private readonly ISanPhamServiece _sanphamsv;
        private readonly IThuongHieuServiece _th;
        private readonly IXuatSuServiece _xx;
        public SanphamController()
        {
            _sanphamsv = new SanPhamServiece();
            _th = new ThuongHieuServiece();
            _xx = new XuatSuServiece();
        }
        [HttpGet("GetAll")]

        public IEnumerable<SanPham> GetAllSanPham()
        {
            return _sanphamsv.GetAll();
        }
        [HttpPost("Create")]
        public bool CreateSanPham(string tensp , Guid idth , Guid idxx)
        {
            SanPham sp = new SanPham()
            {
                Id = Guid.NewGuid(),
                TenSanPham = tensp,
                IdThuongHieu = _th.GetAll().FirstOrDefault(c => c.Id == idth).Id,
                IdXuatSu = _xx.GetAll().FirstOrDefault(c => c.Id == idxx).Id,
                status = 1,
            };
            if(_sanphamsv.GetAll().Any(c => c.IdThuongHieu == idth && c.IdXuatSu == idxx && c.TenSanPham == tensp))
            {
                return false;
            }
            return _sanphamsv.Add(sp);
        }
        [HttpPut("Delete/{Id}")]

        public bool DeleteAsync(Guid Id)
        {
            return _sanphamsv.Del(Id);
        }

    }
}

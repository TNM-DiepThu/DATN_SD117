using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichsuHoaDonController : ControllerBase
    {
        private readonly ILichsuHoaDonService<LichSuHoaDon> _lichsuHoaDonService;
        private MyDbContext _dbContext = new MyDbContext();
        private DbSet<LichSuHoaDon> lshd;

        public LichsuHoaDonController()
        {
            lshd = _dbContext.lichSuHoas;
            LichsuHoaDonService<LichSuHoaDon> all = new LichsuHoaDonService<LichSuHoaDon>(_dbContext, lshd);
            _lichsuHoaDonService = all;
        }
        [HttpGet("GetByID")]
        public LichSuHoaDon GetByID(Guid Id)
        {
            return _lichsuHoaDonService.GetByID(Id);
        }
        // GET: api/<LichsuHoaDonController>
        [HttpGet("get-lichsuhoadon")]
        public IEnumerable<LichSuHoaDon> GetLichsuHoaDon()
        {
            
                return _lichsuHoaDonService.GetAll();
            
        }

       

        // POST api/<LichsuHoaDonController>
        [HttpPost("create-lichsuhoadon")]
        public bool CreateLichsuHoaDon(DateTime ngaytao, string nguoitao, string ghichu, Guid idhoadon, Guid idnguoidung)
        {
            LichSuHoaDon lshd = new LichSuHoaDon();
            lshd.Id = Guid.NewGuid();
            lshd.NgayTao = ngaytao;
            lshd.NguoiTao = nguoitao;
            lshd.GhiChu = ghichu;
            lshd.IdHoaDon = idhoadon;
            lshd.IdNguoiDung = idnguoidung;
            return _lichsuHoaDonService.AddItem(lshd);
        }

        // PUT api/<LichsuHoaDonController>/5
        [HttpPut("update-lichsuhoadon")]
        public bool UpdateLichsuHoaDon(Guid id, DateTime ngaytao, string nguoitao, string ghichu, Guid idhoadon, Guid idnguoidung)
        {

            var lshd = _lichsuHoaDonService.GetAll().First(c => c.Id == id);
            lshd.NgayTao = ngaytao;
            lshd.NguoiTao = nguoitao;
            lshd.GhiChu = ghichu;
            lshd.IdHoaDon = idhoadon;
            lshd.IdNguoiDung = idnguoidung;
            return _lichsuHoaDonService.EditItem(lshd);
        }

        // DELETE api/<LichsuHoaDonController>/5
        [HttpDelete("delete-lichsuhoadon")]
        public bool Delete(Guid id)
        {
            var lshd = _lichsuHoaDonService.GetAll().First(c => c.Id == id);
            return _lichsuHoaDonService.RemoveItem(lshd);
        }
    }
}

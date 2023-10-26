using AppData.data;
using AppData.model;
using AppData.Serviece;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonCTController : ControllerBase
    {
        private readonly IHoaDonCTService<HoaDonChiTiet> _hoaDonCTService;
        private MyDbContext _dbContext = new MyDbContext();
        private DbSet<HoaDonChiTiet> hdct;

        public HoaDonCTController()
        {
            hdct = _dbContext.hoaDonChiTiets;
            HoaDonCTService<HoaDonChiTiet> all = new HoaDonCTService<HoaDonChiTiet>(_dbContext, hdct);
            _hoaDonCTService = all;
        }
        [HttpGet("GetByID")]
        public HoaDonChiTiet GetByID(Guid Id)
        {
            return _hoaDonCTService.GetByID(Id);
        }
        // GET: api/<HoaDonCTController>
        [HttpGet("get-hoadonct")]
        public IEnumerable<HoaDonChiTiet> GetHoaDonCT()
        {
            return _hoaDonCTService.GetAll();
        }


        // POST api/<HoaDonCTController>
        [HttpPost("create-hoadonct")]
        public bool CreateHoaDonCT(int soluong, decimal gia, int trangthai, Guid idhd, Guid idcomboct, Guid idspct)
        {
           
                HoaDonChiTiet hdcts = new HoaDonChiTiet();
                hdcts.Id = Guid.NewGuid();
                hdcts.SoLuong = soluong;
                hdcts.Gia = gia;
                hdcts.status = trangthai;
                hdcts.IDHD = idhd;
                hdcts.IdCombo = idcomboct;
                hdcts.IdSPCT = idspct;
            return _hoaDonCTService.AddItem(hdcts);
            
        }

        // PUT api/<HoaDonCTController>/5
        [HttpPut("update-hoadonct")]
        public bool UpdateHoaDonCT(Guid id, int soluong, decimal gia, int trangthai, Guid idhd, Guid idcomboct, Guid idspct)
        {
            var hdct = _hoaDonCTService.GetAll().First(c => c.Id == id);
            hdct.SoLuong = soluong;
            hdct.Gia = gia;
            hdct.status = trangthai;
            hdct.IDHD = idhd;
            hdct.IdSPCT = idspct;
            hdct.IdCombo = idcomboct;
            if (_hoaDonCTService.EditItem(hdct))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<HoaDonCTController>/5
        [HttpDelete("delete-hoadonct")]
        public bool Delete(Guid id)
        {
            var hdct = _hoaDonCTService.GetAll().First(c => c.Id == id);
            if (_hoaDonCTService.RemoveItem(hdct))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

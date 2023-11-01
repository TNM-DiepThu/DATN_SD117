using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonService<HoaDon> _hoaDonService;
        private MyDbContext _dbContext = new MyDbContext();
        private DbSet<HoaDon> hd;

        public HoaDonController()
        {
            hd = _dbContext.hoaDons;
            HoaDonService<HoaDon> all = new HoaDonService<HoaDon>(_dbContext, hd);
            _hoaDonService = all;
        }
        [HttpGet("GetByID")]
        public HoaDon GetByID(Guid Id)
        {
            return _hoaDonService.GetByID(Id);
        }
        // GET: api/<HoaDonController>
        [HttpGet("get-hoadon")]
        public IEnumerable<HoaDon> GetAllHoaDon()
        {
            return _hoaDonService.GetAll();
        }

        // POST api/<HoaDonController>
        [HttpPost("create-hoadon")]
        public bool CreateHoaDon(string mahd, DateTime ngaytao, int soluong, decimal tongtien, decimal tienvanchuyen, DateTime ngaygiao, DateTime ngaynhan, string nguoinhan, string sdt, string quanhuyen, string tinh, string diachi, DateTime ngaythanhtoan, string ghichu, int trangthai, Guid idnguoidung, Guid idvoucherdetail, Guid idhttt)
        {

            HoaDon hd = new HoaDon();
            hd.Id = Guid.NewGuid();
            hd.IdNguoiDunh = idnguoidung;
            hd.IdVoucherDetail = idvoucherdetail;
            hd.IDHTTT = idhttt;
            hd.MaHD = mahd;
            hd.NgayTao = ngaytao;
            hd.SoLuong = soluong;
            hd.TongTien = tongtien;
            hd.TienVanChuyen = tienvanchuyen;
            hd.NgayGiao = ngaygiao;
            hd.NgayNhan = ngaynhan;
            hd.NguoiNhan = nguoinhan;
            hd.SDT = sdt;
            hd.QuanHuyen = quanhuyen;
            hd.Tinh = tinh;
            hd.DiaChi = diachi;
            hd.NgayThanhToan = ngaythanhtoan;
            hd.GhiChu = ghichu;
            hd.status = trangthai;

            return _hoaDonService.AddItem(hd);
            
        }

        // PUT api/<HoaDonController>/5
        [HttpPut("update-hoadon")]
        public bool UpdateHoaDon(Guid id, string mahd, DateTime ngaytao, int soluong, decimal tongtien, decimal tienvanchuyen, DateTime ngaygiao, DateTime ngaynhan, string nguoinhan, string sdt, string quanhuyen, string tinh, string diachi, DateTime ngaythanhtoan, string ghichu, int trangthai, Guid idnguoidung, Guid idvoucherdetail, Guid idhttt)
        {
            var hd = _hoaDonService.GetAll().First(c => c.Id == id);
           
            hd.MaHD = mahd;
            hd.NgayTao = ngaytao;
            hd.SoLuong = soluong;
            hd.TongTien = tongtien;
            hd.TienVanChuyen = tienvanchuyen;
            hd.NgayGiao = ngaygiao;
            hd.NgayNhan = ngaynhan;
            hd.NguoiNhan = nguoinhan;
            hd.SDT = sdt;
            hd.QuanHuyen = quanhuyen;
            hd.Tinh = tinh;
            hd.DiaChi = diachi;
            hd.NgayThanhToan = ngaythanhtoan;
            hd.GhiChu = ghichu;
            hd.status = trangthai;
            hd.IdNguoiDunh = idnguoidung;
            hd.IdVoucherDetail = idvoucherdetail;
            hd.IDHTTT = idhttt;
            return _hoaDonService.EditItem(hd);
        }

        // DELETE api/<HoaDonController>/5
        [HttpDelete("delete-hoadon")]
        public bool Delete(Guid id)
        {
            var hd = _hoaDonService.GetAll().First(c => c.Id == id);
            return _hoaDonService.RemoveItem(hd);
        }
    }
}

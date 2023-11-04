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
        private readonly IHoaDonService _hoaDonService;
        private MyDbContext _dbContext = new MyDbContext();


        public HoaDonController()
        {
            _hoaDonService = new HoaDonService();
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
        [HttpPost("[action]")]
        public bool CreateHoaDon(DateTime ngaytao, int soluong, decimal tongtien, decimal tienvanchuyen, DateTime ngaygiao, DateTime ngaynhan, string nguoinhan, string sdt, string quanhuyen, string tinh, string diachi, DateTime ngaythanhtoan, string ghichu,  Guid idnguoidung, Guid idvoucherdetail, Guid idhttt)
        {
            HoaDon hd = new HoaDon();
            hd.Id = Guid.NewGuid();
            hd.IdNguoiDunh = idnguoidung;
            hd.IdVoucherDetail = idvoucherdetail;
            hd.IDHTTT = idhttt;
            hd.MaHD = Convert.ToString(hd.Id).Substring(0, 8).ToUpper();
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
            hd.status = 1;
            return _hoaDonService.AddItem(hd);
        }

        // PUT api/<HoaDonController>/5
        [HttpPut("[action]")]
        public bool UpdateHoaDon(Guid id,  DateTime ngaygiao, DateTime ngaynhan, string nguoinhan, string sdt, string quanhuyen, string tinh, string diachi, DateTime ngaythanhtoan, string? ghichu,   Guid idvoucherdetail, Guid idhttt)
        {
            var hd = _hoaDonService.GetAll().First(c => c.Id == id);
          
            hd.NgayTao = DateTime.Now;
            hd.SoLuong = 0;
            hd.TongTien = 0;
            hd.TienVanChuyen = 0;
            hd.NgayGiao = ngaygiao;
            hd.NgayNhan = ngaynhan;
            hd.NguoiNhan = nguoinhan;
            hd.SDT = sdt;
            hd.QuanHuyen = quanhuyen;
            hd.Tinh = tinh;
            hd.DiaChi = diachi;
            hd.NgayThanhToan = ngaythanhtoan;
            hd.GhiChu = ghichu;
            hd.status = 1;
            //hd.IdNguoiDunh = idnguoidung;
            hd.IdVoucherDetail = idvoucherdetail;
            hd.IDHTTT = idhttt;
            return _hoaDonService.EditItem(hd);
        }

        // DELETE api/<HoaDonController>/5
        [HttpPut("[action]")]
        public bool Delete(Guid id)
        {
            var hd = _hoaDonService.GetAll().First(c => c.Id == id);
            return _hoaDonService.RemoveItem(hd);
        }
    }
}

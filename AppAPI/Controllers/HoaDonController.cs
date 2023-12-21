using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.HoaDon;
using AppData.ViewModal.VoucherVM;
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
        private readonly IHoaDonCTService _hdctservice;
        private readonly VoucherViewModelService VoucherviewModel;
        private readonly IGioHangCTService _giohangchitietservice;
        private readonly IVoucherDetailServices _ivoucherdetail;
        private MyDbContext _dbContext = new MyDbContext();


        public HoaDonController()
        {
            _hoaDonService = new HoaDonService();
            _giohangchitietservice = new GioHangCTService();
            VoucherviewModel = new VoucherViewModelService();
            _ivoucherdetail = new VoucherDetailServices();
            _hdctservice = new HoaDonCTService();
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
            return _hoaDonService.GetAll().OrderByDescending(c => c.NgayTao);
        }

        [HttpGet("[action]")]
        public IEnumerable<HoaDon> GetAllHoaDonByIDnguoiDung(Guid id)
        {
            return _hoaDonService.GetAllByIDNguoiDung(id).OrderByDescending(c => c.NgayTao);
        }
        [HttpGet("[action]")]
        public IEnumerable<HoaDon> GetAllHoaDonCho()
        {
            return _hoaDonService.GetAll().Where(c => c.status == 7).ToList();
        }
        [HttpPost("[action]")]
        public bool TaoHoaDonCho(HoaDon hoaDon)
        {
            // tạo hóa đơn
            HoaDon hd = new HoaDon();
            hd.Id = Guid.NewGuid();
            hd.IdNguoiDunh = hoaDon.IdNguoiDunh;
            hd.IdVoucherDetail = hoaDon.IdVoucherDetail;
            hd.IDHTTT = hoaDon.IDHTTT;
            hd.MaHD = Convert.ToString(hd.Id).Substring(0, 8).ToUpper();
            hd.NgayTao = DateTime.Now;
            hd.SoLuong = 0;
            hd.TongTien = 0;
            hd.TienVanChuyen = 0;
            hd.NgayGiao = hoaDon.NgayGiao;
            hd.NgayNhan = hoaDon.NgayNhan;
            hd.NguoiNhan = hoaDon.NguoiNhan;
            hd.SDT = hoaDon.SDT;
            hd.QuanHuyen = hoaDon.QuanHuyen;
            hd.Tinh = hoaDon.Tinh;
            hd.DiaChi = hoaDon.DiaChi;
            hd.NgayThanhToan = hoaDon.NgayThanhToan;
            hd.GhiChu = hoaDon.GhiChu;
            hd.status = 7;
            _hoaDonService.AddItem(hd);

            //tạo hóa đơn chi tiết
            HoaDonChiTiet hdct = new HoaDonChiTiet();

            hdct.Id = Guid.NewGuid();
            hdct.IDHD = hd.Id;
            return _hdctservice.AddItem(hdct);
        }
        [HttpPost("[action]")]
        public string TaoHoaDonKhongCoVoucher(string nguoinhan, string sdt, string quanhuyen, string tinh, string diachi, DateTime ngaythanhtoan, string? ghichu, Guid idnguoidung, Guid idhttt, decimal tienship)
        {
            HoaDon hd = new HoaDon();
            hd.Id = Guid.NewGuid();
            hd.IdNguoiDunh = idnguoidung;
            hd.IdVoucherDetail = null;
            hd.IDHTTT = idhttt;
            hd.MaHD = Convert.ToString(hd.Id).Substring(0, 8).ToUpper();
            hd.NgayTao = DateTime.Now;
            hd.SoLuong = 0;
            hd.TongTien = 0;
            hd.TienVanChuyen = 0;
            hd.NgayGiao = DateTime.Now;
            hd.NgayNhan = DateTime.Now;
            hd.NguoiNhan = nguoinhan;
            hd.SDT = sdt;
            hd.QuanHuyen = quanhuyen;
            hd.Tinh = tinh;
            hd.DiaChi = diachi;
            hd.NgayThanhToan = ngaythanhtoan;
            hd.GhiChu = ghichu;
            hd.status = 1;
            _hoaDonService.AddItem(hd);

            List<GioHangChiTiet> ghct = _giohangchitietservice.GetAllGioHangTheoNguoiDungDangNhap(idnguoidung);
            if (ghct.Count > 0)
            {
                foreach (var item in ghct)
                {
                    HoaDonChiTiet hoct = new HoaDonChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        IDHD = hd.Id,
                        IdCombo = item.IdComboChiTiet,
                        IdSPCT = item.IdSanPhamChiTiet,
                        SoLuong = item.SoLuong,
                        Gia = item.DonGia,
                    };
                    _hdctservice.AddItem(hoct);
                    _giohangchitietservice.Del(item.Id);
                }
                var lsthoadonct = _hdctservice.GetAllByIdHd(hd.Id);
                HoaDon hdupdate = _hoaDonService.GetByID(hd.Id);
                hdupdate.SoLuong = lsthoadonct.Sum(c => c.SoLuong);
                hdupdate.TongTien = lsthoadonct.Sum(c => c.SoLuong * c.Gia);
                hdupdate.TienVanChuyen = tienship;
                hdupdate.NgayGiao = DateTime.Now;
                hdupdate.NgayNhan = DateTime.Now;
                hdupdate.NguoiNhan = nguoinhan;
                hdupdate.SDT = sdt;
                hdupdate.QuanHuyen = quanhuyen;
                hdupdate.Tinh = tinh;
                hdupdate.DiaChi = diachi;
                hdupdate.NgayThanhToan = ngaythanhtoan;
                hdupdate.GhiChu = ghichu;
                hdupdate.status = 1;
                 _hoaDonService.EditItem(hdupdate);
                return "Tạo hóa đơn thành công";

            }
            else
            {
                return "Tạo hóa đơn thất bại";
            }
        }
        // POST api/<HoaDonController>
        [HttpPost("[action]")]
        public string TaoHoaDonCoVoucher(string nguoinhan, string sdt, string quanhuyen, string tinh, string diachi, DateTime ngaythanhtoan, string? ghichu, Guid idnguoidung, Guid? idvoucherdetail, Guid idhttt, decimal tienship)
        {
            VoucherDetailHoanThien voucher = VoucherviewModel.GetListVoucherViewModel().FirstOrDefault(c => c.IDNguoiDuong == idnguoidung && c.IDVoucherDetail == idvoucherdetail);
            if(voucher.Soluong <= 0)
            {
                return "Voucher đã hết";
            } else
            {
                //Gọi list giỏ hàng của người dùng đăng nhập
                List<GioHangChiTiet> ghct = _giohangchitietservice.GetAllGioHangTheoNguoiDungDangNhap(idnguoidung);
                // thực hiện check điều kiện với tổng giá 

                // tạo hóa đơn
                HoaDon hd = new HoaDon();
                hd.Id = Guid.NewGuid();
                hd.IdNguoiDunh = idnguoidung;
                hd.IdVoucherDetail = idvoucherdetail;
                hd.IDHTTT = idhttt;
                hd.MaHD = Convert.ToString(hd.Id).Substring(0, 8).ToUpper();
                hd.NgayTao = DateTime.Now;
                hd.SoLuong = 0;
                hd.TongTien = 0;
                hd.TienVanChuyen = 0;
                hd.NgayGiao = DateTime.Now;
                hd.NgayNhan = DateTime.Now;
                hd.NguoiNhan = nguoinhan;
                hd.SDT = sdt;
                hd.QuanHuyen = quanhuyen;
                hd.Tinh = tinh;
                hd.DiaChi = diachi;
                hd.NgayThanhToan = ngaythanhtoan;
                hd.GhiChu = ghichu;
                hd.status = 1;
                _hoaDonService.AddItem(hd);

                
                if (ghct.Count > 0)
                {
                    foreach (var item in ghct)
                    {
                        HoaDonChiTiet hoct = new HoaDonChiTiet()
                        {
                            Id = Guid.NewGuid(),
                            IDHD = hd.Id,
                            IdCombo = item.IdComboChiTiet,
                            IdSPCT = item.IdSanPhamChiTiet,
                            SoLuong = item.SoLuong,
                            Gia = item.DonGia,
                        };
                        _hdctservice.AddItem(hoct);
                        _giohangchitietservice.Del(item.Id);
                    }
                    var lsthoadonct = _hdctservice.GetAllByIdHd(hd.Id);
                    HoaDon hdupdate = _hoaDonService.GetByID(hd.Id);
                    hdupdate.SoLuong = lsthoadonct.Sum(c => c.SoLuong);
                    hdupdate.TongTien = lsthoadonct.Sum(c => c.SoLuong * c.Gia) - (lsthoadonct.Sum(c => c.SoLuong * c.Gia) * voucher.GiaTriVoucher);
                    hdupdate.TienVanChuyen = tienship;
                    hdupdate.NgayGiao = DateTime.Now;
                    hdupdate.NgayNhan = DateTime.Now;
                    hdupdate.NguoiNhan = nguoinhan;
                    hdupdate.SDT = sdt;
                    hdupdate.QuanHuyen = quanhuyen;
                    hdupdate.Tinh = tinh;
                    hdupdate.DiaChi = diachi;
                    hdupdate.NgayThanhToan = ngaythanhtoan;
                    hdupdate.GhiChu = ghichu;
                    hdupdate.status = 1;
                    _hoaDonService.EditItem(hdupdate);

                    return "Tạo hóa đơn thành công.";

                }
                else
                {
                    return "Tạo hóa đơn thất bại";
                }
            }
          
        }

        // PUT api/<HoaDonController>/5
        [HttpPut("[action]")]
        public bool UpdateHoaDon(int sl, decimal tienvanchuyen, decimal tongtien, Guid id, DateTime ngaygiao, DateTime ngaynhan, string nguoinhan, string sdt, string quanhuyen, string tinh, string diachi, DateTime ngaythanhtoan, string? ghichu, int trangthai, Guid? idvoucherdetail, Guid idhttt)
        {
            var hd = _hoaDonService.GetAll().First(c => c.Id == id);

            hd.NgayTao = DateTime.Now;
            hd.SoLuong = sl;
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

        [HttpPut("[action]")]
        public bool UpdateDaXacNhan(Guid idhb)
        {
            return _hoaDonService.UpdateDaXacNhan(idhb);
        }
        [HttpPut("[action]")]
        public bool UpdateChoLayHang(Guid idhb)
        {
            return _hoaDonService.UpdateChoLayHang(idhb);
        }
        [HttpPut("[action]")]
        public bool UpdateDaLayhang(Guid idhb)
        {
            return _hoaDonService.UpdateDaLayHang(idhb);
        }
        [HttpPut("[action]")]
        public bool UpdateDaThanhToan(Guid idhb)
        {
            return _hoaDonService.UpdateDaThanhToan(idhb);
        }
        [HttpPut("[action]")]
        public bool UpdateHuyHang(Guid idhb)
        {
            return _hoaDonService.UpdateHuy(idhb);
        }

        [HttpPut("[action]")]
        public bool UpdateDaNhanHang(Guid idhb)
        {
            return _hoaDonService.UpdateDaNhanHang(idhb);
        }

        [HttpPut("[action]")]
        public bool UpdateHoaDonCho(Guid idhb)
        {
            return _hoaDonService.UpdateHDCho(idhb);
        }
    }
}

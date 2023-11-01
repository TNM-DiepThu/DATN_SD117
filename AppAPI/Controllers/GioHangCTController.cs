using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.GioHangChiTietViewModel;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangCTController : ControllerBase
    {
        private readonly IGioHangCTService _GHCT;
        private readonly IGioHangService _giohangservice;
        private readonly GioHangChiTietViewModelService _giohangctviewmodelservice;
        private readonly ISanPhamChiTietServiece _SPCT;
        private readonly IComboChiTietService _combochiTietservice;
        private readonly IGioHangService _Ga;
        public GioHangCTController()
        {
            _GHCT = new GioHangCTService();
            _SPCT = new SanPhamChiTietServiece();
            _giohangservice = new GioHangService();
            _combochiTietservice = new ComBoChiTietService();
            _giohangctviewmodelservice = new GioHangChiTietViewModelService();
        }


        [HttpGet("[action]")]
        public IEnumerable<GioHangChiTietViewModel> GetAllFullGioHangChiTiet(Guid IDnguoiDung)
        {
            return _giohangctviewmodelservice.GetAllListGioHang(IDnguoiDung);
        }

        [HttpGet("[action]")]

        public IEnumerable<GioHangChiTiet> GetAllTheoLogin(Guid Idnguoidung)
        {
            return _GHCT.GetAllGioHangTheoNguoiDungDangNhap(Idnguoidung);
        }

        [HttpPost("[action]")]

        public string UpdateSoLuong(Guid idnguoidung, Guid idghct, int soluong)
        {
            GioHangChiTiet ghct = _GHCT.GetAllGioHangTheoNguoiDungDangNhap(idnguoidung).FirstOrDefault(c => c.Id == idghct);
            if (ghct.IdComboChiTiet == null)
            {
                SanPhamChiTiet spct = _SPCT.GetAll().FirstOrDefault(c => c.Id == ghct.IdSanPhamChiTiet);
                if (soluong > spct.SoLuong)
                {
                    return "San pham nay khong du so luong nhu ban yeu cau";
                }
                else
                {
                    ghct.Id = ghct.Id;
                    ghct.SoLuong = soluong;
                    ghct.IdSanPhamChiTiet = spct.Id;
                    ghct.DonGia = spct.GiaBan;
                    ghct.IdComboChiTiet = null;
                    _GHCT.EditSoluong(idghct, soluong);

                    return "Cap nhat so luong thanh cong";
                }
            }
            else if (ghct.IdSanPhamChiTiet == null)
            {
                ComboChiTiet spct = _combochiTietservice.GetAll().FirstOrDefault(c => c.Id == ghct.IdComboChiTiet);
                if (soluong > spct.SoLuongSanPham)
                {
                    return "San pham nay khong du so luong nhu ban yeu cau";
                }
                else
                {
                    ghct.Id = ghct.Id;
                    ghct.SoLuong = soluong;
                    ghct.IdSanPhamChiTiet = null;
                    ghct.DonGia = spct.GiaBan;
                    ghct.IdComboChiTiet = ghct.IdComboChiTiet;

                    _GHCT.EditSoluong(idghct, soluong);
                    return "Cap nhat so luong thanh cong";
                }

            }
            else
            {
                return "Them so luong that bai.";
            }

        }
        [HttpDelete("[action]")]
        public bool XoaSanPham(Guid id)
        {
            return _GHCT.Del(id);
        }
    }
}

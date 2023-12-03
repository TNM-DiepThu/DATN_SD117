using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComBoChiTietController : ControllerBase
    {
        private readonly IComboService Cb;
        private readonly ComBoChiTietViewModelService _combochitietViewModel;
        private readonly IComboChiTietService CbChiTiet;
        private readonly IGioHangCTService _gioHangCTService;
        private readonly ISizeServiece _sizeService;
        private readonly IMauSacServiece _mauSacServiece;
        private readonly IGioHangService _giohangService;
        private readonly ISanPhamChiTietServiece sanPhamChiTietServiece;
        private readonly SanPhamChiTietViewModelService _spctmv;
        public ComBoChiTietController()
        {
            Cb = new ComboService();
            CbChiTiet = new ComBoChiTietService();
            _combochitietViewModel = new ComBoChiTietViewModelService();
            _spctmv = new SanPhamChiTietViewModelService();
            _sizeService = new SizeServiece();
            _giohangService = new GioHangService();
            _mauSacServiece = new MauSacServiece();
            _gioHangCTService = new GioHangCTService();
            sanPhamChiTietServiece = new SanPhamChiTietServiece();


        }
        [HttpGet("GetAll")]

        public IEnumerable<ComboChiTiet> GetAllAsync()
        {
            return CbChiTiet.GetAll();
        }
        [HttpGet("[action]")]
        public ComboChiTiet GetByID(Guid ID)
        {
            return CbChiTiet.GetById(ID);
        }
        [HttpPost("[action]")]
        public string Create(int soluongsanpham, string tencombo, Guid IDcombo, Guid IDCTSP, int soluongcombo)
        {
            var spct = sanPhamChiTietServiece.GetAll().FirstOrDefault(c => c.Id == IDCTSP);

            var combo = Cb.GetAll().FirstOrDefault(c => c.Id == IDcombo);
            try
            {
                if (soluongsanpham * soluongcombo > spct.SoLuong)
                {
                    return "Số lượng nhiều hơn số lượng sản phẩm.";
                }
                else if (soluongsanpham == 0)
                {
                    return "Bạn chưa nhập số lượng.";
                }
                else if (spct.SoLuong == 0 && spct.status == 0)
                {
                    return "Sản phẩm đã hết.";
                }
                else if (CbChiTiet.GetAll().Any(c => c.IdCombo == IDcombo && c.IdSPCT == IDCTSP))
                {
                    return "Combo đã tồm tại" + CbChiTiet.GetAll().FirstOrDefault(c => c.IdCombo == IDcombo && c.IdSPCT == IDCTSP).TenComboct;
                }
                else
                {
                    ComboChiTiet comboct = new ComboChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        SoLuongSanPham = soluongsanpham,
                        SoLuongCombo = soluongcombo,
                        TenComboct = tencombo.Substring(0, Math.Min(tencombo.Length, 1000)),
                        GiaGoc = (soluongsanpham * spct.GiaBan),
                        TienGiamGia = (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100),
                        GiaBan = (soluongsanpham * spct.GiaBan) - (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100),
                        IdCombo = combo.Id,
                        IdSPCT = spct.Id,
                        TrangThai = 1
                    };
                    CbChiTiet.Add(comboct);

                    spct.SoLuong = spct.SoLuong - (comboct.SoLuongSanPham * comboct.SoLuongCombo);
                    sanPhamChiTietServiece.Edit(spct.Id, spct);
                    return "Thêm thành công";
                }
            }
            catch
            {
                return "Lỗi. Mời bạn thao tác lại.";
            }
        }
        [HttpDelete("[action]")]

        public bool DeleteAsync(Guid Id)
        {
            var combo = _combochitietViewModel.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == Id);
            combo.TrangThai = 0;
            return CbChiTiet.Del(combo.Id);
        }

        [HttpPost("[action]")]

        public string UpdateCombo(Guid id, int soluongsanpham, string tencombo, Guid IDcombo, Guid IDCTSP, int soluongcombo, int trangthai)
        {
            var spct = sanPhamChiTietServiece.GetAll().FirstOrDefault(c => c.Id == IDCTSP);

            var combo = Cb.GetAll().FirstOrDefault(c => c.Id == IDcombo);
            try
            {
                if (soluongsanpham * soluongcombo > spct.SoLuong)
                {
                    return "Số lượng nhiều hơn số lượng sản phẩm.";
                }
                else if (soluongsanpham == 0)
                {
                    return "Bạn chưa nhập số lượng.";
                }
                else if (spct.SoLuong == 0 && spct.status == 0)
                {
                    return "Sản phẩm đã hết.";
                }
                else
                {
                    var comboct = CbChiTiet.GetAll().FirstOrDefault(c => c.Id == id);

                    spct.SoLuong = (comboct.SoLuongSanPham * comboct.SoLuongCombo) + spct.SoLuong; // lấy lại só lượng khi update

                    comboct.SoLuongSanPham = soluongsanpham;
                    comboct.SoLuongCombo = soluongcombo;
                    comboct.TenComboct = tencombo;
                    comboct.GiaGoc = (soluongsanpham * spct.GiaBan);
                    comboct.TienGiamGia = (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100);
                    comboct.GiaBan = (soluongsanpham * spct.GiaBan) - (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100);
                    comboct.IdCombo = combo.Id;
                    comboct.IdSPCT = spct.Id;
                    comboct.TrangThai = trangthai;

                    CbChiTiet.Edit(comboct.Id, comboct);

                    spct.SoLuong = spct.SoLuong - (comboct.SoLuongSanPham * comboct.SoLuongCombo);
                    sanPhamChiTietServiece.Edit(spct.Id, spct);
                    return "Sửa thành công";
                }
            }
            catch
            {
                return "Lỗi. Mời bạn thao tác lại.";
            }
        }
        [HttpGet("[action]")]
        public List<ComBoChiTietViewModel> GetallFullComboCt()
        {
            return _combochitietViewModel.GetAllComBoChiTiet();
        }
        [HttpGet("[action]")]
        public List<ComBoChiTietViewModel> GetallFullComboCtByName(string name)
        {
            return _combochitietViewModel.GetAllComBoChiTietByName(name);
        }
        [HttpGet("[action]")]
        public ComBoChiTietViewModel GetallFullComboCtByID(Guid id)
        {
            return _combochitietViewModel.GetAllComBoChiTietByID(id);
        }
        [HttpPost("[action]")]
        public string ThemComBoVaoGioHang(Guid idnguoidung, Guid IDComboCt, int SoLuong)
        {
            if (_giohangService.GetAll().Any(c => c.IdNguoiDung == idnguoidung) == false)
            {
                _giohangService.Add(idnguoidung);
            }
            var checkCombo = _gioHangCTService.GetAllGioHangTheoNguoiDungDangNhap(idnguoidung).FirstOrDefault(c => c.IdComboChiTiet == IDComboCt);
            if (checkCombo == null)
            {
                GioHangChiTiet gioHangChiTiet = new GioHangChiTiet()
                {
                    Id = Guid.NewGuid(),
                    IdComboChiTiet = IDComboCt,
                    IdSanPhamChiTiet = null,
                    IdGioHang = _giohangService.GetAll().FirstOrDefault(c => c.IdNguoiDung == idnguoidung).Id,
                    SoLuong = SoLuong,
                    DonGia = _combochitietViewModel.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == IDComboCt).ThanhTienComBo,
                };
                _gioHangCTService.Add(gioHangChiTiet);
                return "Thêm mới thành công";
            }
            else
            {
                checkCombo.SoLuong = SoLuong;
                _gioHangCTService.Edit(checkCombo.Id, checkCombo);
                return "Tăng số lượng thành công";
            }

        }
        [HttpGet("[action]")]
        public List<MauSac> GetAllMauSacTheoTenSanPham(Guid idcomboct)
        {
            List<MauSac> lstmau = new List<MauSac>();
            var combo = _combochitietViewModel.GetAllComBoChiTietByID(idcomboct);
            var lstcomboct = _combochitietViewModel.GetAllComBoChiTiet().Where(c => c.TenComBo == combo.TenComBo).Distinct().Select(c => c.MauSac).ToList();
            if(lstcomboct.Count > 0)
            {
                foreach(var c in lstcomboct)
                {
                    var mausac = _mauSacServiece.GetAll().FirstOrDefault(c => c.TenMauSac == c.TenMauSac);
                    lstmau.Add(mausac);
                }
            }
            return lstmau.ToList();
        }
        [HttpGet("[action]")]
        public List<Size> GetAllSizeTheoTenSanPham(Guid idcomboct)
        {
            List<Size> lstsize = new List<Size>();
            var combo = _combochitietViewModel.GetAllComBoChiTietByID(idcomboct);
            var lstcomboct = _combochitietViewModel.GetAllComBoChiTiet().Where(c => c.TenComBo == combo.TenComBo).Distinct().Select(c => c.Size).ToList();
            if (lstcomboct.Count > 0)
            {
                foreach (var c in lstcomboct)
                {
                    var size = _sizeService.GetAll().FirstOrDefault(c => c.SizeName == c.SizeName);
                    lstsize.Add(size);
                }
            }
            return lstsize.ToList();
        }
    }
}

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
        private readonly ISanPhamChiTietServiece sanPhamChiTietServiece;
        private readonly SanPhamChiTietViewModelService _spctmv;
        public ComBoChiTietController()
        {
            Cb = new ComboService();
            CbChiTiet = new ComBoChiTietService();
            _combochitietViewModel = new ComBoChiTietViewModelService();
            _spctmv = new SanPhamChiTietViewModelService();
            sanPhamChiTietServiece = new SanPhamChiTietServiece();


        }
        [HttpGet("GetAll")]

        public IEnumerable<ComboChiTiet> GetAllAsync()
        {
            return CbChiTiet.GetAll();
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
                else
                {
                    ComboChiTiet comboct = new ComboChiTiet()
                    {
                        Id = Guid.NewGuid(),
                        SoLuongSanPham = soluongsanpham,
                        SoLuongCombo = soluongcombo,
                        TenComboct = tencombo,
                        GiaGoc = (soluongsanpham * spct.GiaBan),
                        TienGiamGia = (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100),
                        GiaBan = (soluongsanpham * spct.GiaBan) - (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100),
                        IdCombo = combo.Id,
                        IdSPCT = spct.Id,
                    };
                    CbChiTiet.Add(comboct);

                    spct.SoLuong = spct.SoLuong - comboct.SoLuongSanPham;
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
            return CbChiTiet.Del(Id);
        }

        [HttpPut("[action]")]

        public string UpdateAsync(Guid id, int soluongsanpham, string tencombo, Guid IDcombo, Guid IDCTSP, int soluongcombo)
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
                    comboct.SoLuongSanPham = soluongsanpham;
                    comboct.SoLuongCombo = soluongcombo;
                    comboct.TenComboct = tencombo;
                    comboct.GiaGoc = (soluongsanpham * spct.GiaBan);
                    comboct.TienGiamGia = (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100);
                    comboct.GiaBan = (soluongsanpham * spct.GiaBan) - (soluongsanpham * spct.GiaBan * combo.PhanTramGiam / 100);
                    comboct.IdCombo = combo.Id;
                    comboct.IdSPCT = spct.Id;

                    CbChiTiet.Edit(comboct.Id, comboct);

                    spct.SoLuong = spct.SoLuong - comboct.SoLuongSanPham;
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
    }
}

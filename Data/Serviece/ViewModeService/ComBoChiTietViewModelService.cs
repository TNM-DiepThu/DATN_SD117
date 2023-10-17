using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.SanPhamChiTietVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.ViewModeService
{
    public class ComBoChiTietViewModelService
    {
        private readonly SanPhamChiTietViewModelService _sanphamctviewmodelservice;
        private readonly IComboService _conboservice;
        private readonly IComboChiTietService _comboctservice;

        public ComBoChiTietViewModelService()
        {
            _sanphamctviewmodelservice  = new SanPhamChiTietViewModelService();
            _conboservice = new ComboService();
            _comboctservice = new ComBoChiTietService();
        }

        public List<ComBoChiTietViewModel> GetAllComBoChiTiet()
        {
            var combochitiet = from a in _comboctservice.GetAll()
                               join b in _sanphamctviewmodelservice.GetAll() on a.IdSPCT equals b.Id
                               join c in _conboservice.GetAll() on a.IdCombo equals c.Id
                               select new ComBoChiTietViewModel
                               {
                                   Id = a.Id,
                                   TenComBo = c.TenCombo,
                                   TenSp = b.TenSP,
                                   GiaGoc = a.SoLuongSanPham * b.GiaBan,
                                   GiaTrịGiam = c.TienGiamGia,
                                   ThanhTienComBo = (a.SoLuongSanPham * b.GiaBan) - (decimal)(a.SoLuongSanPham * b.GiaBan * c.TienGiamGia / 100),
                                   SoluongSanpham = a.SoLuongSanPham
                               };
            return combochitiet.ToList();
        }
        public List<ComBoChiTietViewModel> GetAllComBoChiTietByName(string name)
        {
            var combochitiet = from a in _comboctservice.GetAll()
                               join b in _sanphamctviewmodelservice.GetAll() on a.IdSPCT equals b.Id
                               join c in _conboservice.GetAll() on a.IdCombo equals c.Id
                               select new ComBoChiTietViewModel
                               {
                                   Id = a.Id,
                                   TenComBo = c.TenCombo,
                                   TenSp = b.TenSP,
                                   GiaGoc = a.SoLuongSanPham * b.GiaBan,
                                   GiaTrịGiam = c.TienGiamGia,
                                   ThanhTienComBo = (a.SoLuongSanPham * b.GiaBan) - (decimal)(a.SoLuongSanPham * b.GiaBan * c.TienGiamGia / 100),
                                   SoluongSanpham = a.SoLuongSanPham
                               };
            return combochitiet.Where(c => c.TenComBo.Contains(name)).ToList();
        }
    }
}

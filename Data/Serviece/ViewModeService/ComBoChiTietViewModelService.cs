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
                               join b in _sanphamctviewmodelservice.GetWithOneImage() on a.IdSPCT equals b.Id
                               join c in _conboservice.GetAll() on a.IdCombo equals c.Id
                               select new ComBoChiTietViewModel
                               {
                                   Id = a.Id,
                                   TenComBo = a.TenComboct,
                                   TenSp = b.TenSP,
                                   MauSac = b.MauSac,
                                   Size = b.Size,
                                   GiaGoc = a.GiaGoc,
                                   PathAnh = b.Images != null ? b.Images[0].Connect : null,
                                   SoluongCombo = a.SoLuongCombo,
                                   SoluongSanpham = a.SoLuongSanPham,
                                   TienGiamGia = a.TienGiamGia,
                                   ThanhTienComBo = a.GiaBan,
                                   TrangThai = a.TrangThai,
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
                                   TenComBo = a.TenComboct,
                                   TenSp = b.TenSP,
                                   MauSac = b.MauSac,
                                   Size = b.Size,
                                   GiaGoc = a.GiaGoc,
                                   SoluongCombo = a.SoLuongCombo,
                                   PathAnh = b.Images != null ? b.Images[0].Connect : null,
                                   SoluongSanpham = a.SoLuongSanPham,
                                   TienGiamGia = a.TienGiamGia,
                                   ThanhTienComBo = a.GiaBan,
                                   TrangThai = a.TrangThai,
                               };
            return combochitiet.Where(c => c.TenComBo.Contains(name)).ToList();
        }

        public ComBoChiTietViewModel GetAllComBoChiTietByID(Guid Id)
        {
            var combochitiet = from a in _comboctservice.GetAll()
                               join b in _sanphamctviewmodelservice.GetAll() on a.IdSPCT equals b.Id
                               join c in _conboservice.GetAll() on a.IdCombo equals c.Id
                               select new ComBoChiTietViewModel
                               {
                                   Id = a.Id,
                                   TenComBo = a.TenComboct,
                                   TenSp = b.TenSP,
                                   GiaGoc = a.GiaGoc,
                                   MauSac = b.MauSac,
                                   Size = b.Size,
                                   PathAnh = b.Images != null ? b.Images[0].Connect : null,
                                   SoluongCombo = a.SoLuongCombo,
                                   SoluongSanpham = a.SoLuongSanPham,
                                   TienGiamGia = a.TienGiamGia,
                                   ThanhTienComBo = a.GiaBan,
                                   TrangThai = a.TrangThai,
                               };
            return combochitiet.FirstOrDefault(c => c.Id == Id);
        }
    }
}

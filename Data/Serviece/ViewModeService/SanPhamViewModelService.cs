using AppData.model;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.ViewModeService
{
    public class SanPhamViewModelService
    {
        private readonly IThuongHieuServiece _thuonghieu;
        private readonly ISanPhamServiece _sanpham;
        private readonly IXuatSuServiece _xuatsu;
        public SanPhamViewModelService()
        {
            _thuonghieu = new ThuongHieuServiece();
            _xuatsu = new XuatSuServiece(); 
            _sanpham = new SanPhamServiece();
        }

        public List<SanPhamViewModel> GetAllSanPham()
        {
            var sanpham = from a in _sanpham.GetAll()
                          join b in _thuonghieu.GetAll() on a.IdThuongHieu equals b.Id
                          join c in _xuatsu.GetAll() on a.IdXuatSu equals c.Id
                          select new SanPhamViewModel
                          {
                              Id = a.Id,
                              ThuongHieu = b.TenThuongHieu,
                              TenSanPham = a.TenSanPham,
                              XuatSu = c.TenXuatSu,
                              status = a.status,
                          };
            return sanpham.ToList();
        }
        public SanPhamViewModel GetById(Guid id)
        {
            var sanpham = from a in _sanpham.GetAll()
                          join b in _thuonghieu.GetAll() on a.IdThuongHieu equals b.Id
                          join c in _xuatsu.GetAll() on a.IdXuatSu equals c.Id
                          select new SanPhamViewModel
                          {
                              Id = a.Id,
                              ThuongHieu = b.TenThuongHieu,
                              TenSanPham = a.TenSanPham,
                              XuatSu = c.TenXuatSu,
                          };
            return sanpham.FirstOrDefault(c => c.Id == id);
        }

    }
}

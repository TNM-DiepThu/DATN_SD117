using AppData.data;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.GioHangChiTietViewModel;
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
    public class GioHangChiTietViewModelService
    {
        private readonly MyDbContext _context;
        private readonly IGioHangCTService _giohangctservice;
        private readonly SanPhamChiTietViewModelService _spctviewmodelservice;
        private readonly IGioHangService _giohangservice;
        private readonly ISanPhamChiTietServiece _sanphamctservice;
        private readonly ComBoChiTietViewModelService _comboctservice;
        public GioHangChiTietViewModelService()
        {
            _context = new MyDbContext();
            _giohangservice = new GioHangService();
            _spctviewmodelservice = new SanPhamChiTietViewModelService();
            _giohangctservice = new GioHangCTService();
            _sanphamctservice = new SanPhamChiTietServiece();
            _comboctservice = new ComBoChiTietViewModelService();
        }
        public List<GioHangChiTietViewModel> GetAllListGioHang()
        {

            var lst = from a in _giohangctservice.GetAll()
                      join b in _giohangservice.GetAll() on a.IdGioHang equals b.Id
                      join c in _spctviewmodelservice.GetAll() on a.IdSanPhamChiTiet equals c.Id
                      join d in _comboctservice.GetAllComBoChiTiet() on a.IdComboChiTiet equals d.Id
                      select new GioHangChiTietViewModel
                      {
                          ID = a.Id,
                          IDGH = b.Id,
                          IDSanPham = _spctviewmodelservice.GetAll().FirstOrDefault(c => c.Id == a.IdSanPhamChiTiet).Id != null ? c.Id : _comboctservice.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == a.IdComboChiTiet).Id,
                          TenSanPham = _spctviewmodelservice.GetAll().FirstOrDefault(c => c.Id == a.IdSanPhamChiTiet).TenSP != null ? c.TenSP : _comboctservice.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == a.IdComboChiTiet).TenComBo,
                          Soluong = a.SoLuong,
                          GiaSanPham = a.DonGia,
                      };
            return lst.ToList();
        }

    }
}

using AppData.data;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.GioHangChiTietViewModel;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{
    public class GioHangChiTietViewModelService
    {
        private readonly MyDbContext _context;
        private readonly IGioHangCTService _giohangctservice;
        private readonly IGioHangService _giohangservice;
        private readonly ISanPhamChiTietServiece _sanphamctservice;
        private readonly IComboChiTietService _comboctservice;
        public GioHangChiTietViewModelService()
        {
            _context = new MyDbContext();
            _giohangservice = new GioHangService();
            _giohangctservice = new GioHangCTService();
            _sanphamctservice = new SanPhamChiTietServiece();
            _comboctservice = new  ComBoChiTietService();
        }
        public List<GioHangChiTietViewModel> GetAllListGioHang()
        {
            
            //var lst = from a in _giohangctservice.GetAll()
            //          join b in _giohangservice.GetAll() on a.IdGioHang equals b.Id
            //          join c in _sanphamctservice.GetAll() on a.IdSanPhamChiTiet equals c.Id
            //          join d in _comboctservice.GetAll() on a.IdComboChiTiet equals d.Id
            //          select new GioHangChiTietViewModel{
            //              ID = a.Id,
            //              IDGH = b.Id,
            //              IDSanPham = ,
            //};
            return new List<GioHangChiTietViewModel>();
        }

    }
}

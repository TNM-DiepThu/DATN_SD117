using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.HoaDon;
using Bill.Serviece.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.ViewModeService
{
    public class HoaDonChiTietViewModelService
    {
        private readonly IHoaDonService _hoaDonService;
        private readonly IHoaDonCTService _hoaDonCTService;
        private readonly GioHangChiTietViewModelService _gioHangCTService;
        private readonly SanPhamChiTietViewModelService _spctvmservice;
        private readonly ComBoChiTietViewModelService _comboCTService;
        private readonly MyDbContext _context;
        public HoaDonChiTietViewModelService()
        {
            _context = new MyDbContext();
            _hoaDonService = new HoaDonService();
            _hoaDonCTService = new HoaDonCTService();
            _gioHangCTService = new GioHangChiTietViewModelService();
            _spctvmservice = new SanPhamChiTietViewModelService();
            _comboCTService = new ComBoChiTietViewModelService();
        }

        public List<HoaDonCTViewModel> GetAllHoaDonChiTiet(Guid idHoaDon)
        {
            List<HoaDonChiTiet> hoaDonChiTiet = _hoaDonCTService.GetAllByIdHd(idHoaDon).ToList();
            List<HoaDonCTViewModel> newlist = new List<HoaDonCTViewModel>();
            foreach (var item in hoaDonChiTiet)
            {
                if (item.IdCombo == null)
                {
                    var list = from a in hoaDonChiTiet
                               join b in _spctvmservice.GetAll() on a.IdSPCT equals b.Id
                               join c in _hoaDonService.GetAll() on a.IDHD equals c.Id
                               select new HoaDonCTViewModel
                               {
                                   IDhdCt = a.Id,
                                   IDHd = a.IDHD,
                                   IDSpct = a.IdSPCT,
                                   TenSP = _spctvmservice.GetAll().FirstOrDefault(c => c.Id == a.IdSPCT).TenSP,
                                   SoLuong = a.SoLuong,
                                   GiaGoc = a.Gia,
                                   TiemGiam = 0,
                               };
                    newlist.AddRange(list);
                }
                else if (item.IdSPCT == null)
                {
                    var list = from a in hoaDonChiTiet
                               join b in _comboCTService.GetAllComBoChiTiet() on a.IdCombo equals b.Id
                               join c in _hoaDonService.GetAll() on a.IDHD equals c.Id
                               select new HoaDonCTViewModel
                               {
                                   IDhdCt = a.Id,
                                   IDHd = a.IDHD,
                                   IDSpct = a.IdSPCT,
                                   TenSP = _comboCTService.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == a.IdCombo).TenComBo,
                                   SoLuong = a.SoLuong,
                                   GiaGoc = a.Gia,
                                   TiemGiam = a.Gia - (a.Gia * b.GiaTrịGiam),
                               };
                    newlist.AddRange(list);
                }
            }
            return newlist.ToList();
        }
    }
}

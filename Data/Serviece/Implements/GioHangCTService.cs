using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{
    public class GioHangCTService : IGioHangCTService
    {
        private readonly MyDbContext _context;
        private readonly ISanPhamChiTietServiece _SP;
        private readonly IGioHangService _GH;
        private readonly IComboChiTietService _CB;
        public GioHangCTService()
        {
            _context = new MyDbContext();
            _SP = new SanPhamChiTietServiece();
            _GH = new GioHangService();
            _CB = new ComBoChiTietService();
        }

        public bool Add(GioHangChiTiet p)
        {
            try
            {
                _context.gioHangChiTiets.Add(p);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public bool Del(Guid id)
        {

            try
            {
                GioHangChiTiet obj = _context.gioHangChiTiets.FirstOrDefault(c => c.Id == id);
                _context.gioHangChiTiets.Remove(obj);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool EditSoluong(Guid idghct, int soluong)
        {
            GioHangChiTiet ghct = _context.gioHangChiTiets.FirstOrDefault(c => c.Id == idghct);
            ghct.SoLuong = soluong;
            _context.gioHangChiTiets.Update(ghct);
            _context.SaveChangesAsync();
            return true;
        }
        public List<GioHangChiTiet> GetAllGioHangTheoNguoiDungDangNhap(Guid idnguoidung)
        {
            var idgh = _GH.GetAll().FirstOrDefault(c => c.IdNguoiDung == idnguoidung).Id ;
            return _context.gioHangChiTiets.Where(c => c.IdGioHang == idgh).ToList();
        }
        public bool Edit(Guid idghct, GioHangChiTiet p)
        {
            GioHangChiTiet ghct = _context.gioHangChiTiets.FirstOrDefault(c => c.Id == idghct);
            if(ghct == null) return false;
            ghct.SoLuong = p.SoLuong;
            _context.gioHangChiTiets.Update(ghct);
            _context.SaveChangesAsync();
            return true;
        }
    }
}

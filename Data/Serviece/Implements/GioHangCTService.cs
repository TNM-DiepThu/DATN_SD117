using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

                GioHangChiTiet GH = new GioHangChiTiet()
                {
                    Id = Guid.NewGuid(),
                    SoLuong = p.SoLuong,
                    DonGia = p.DonGia,
                    IdSanPhamChiTiet = _SP.GetAll().FirstOrDefault(c => c.Id == p.IdSanPhamChiTiet).Id,
                    IdGioHang = _GH.GetAll().FirstOrDefault(c => c.Id == p.IdGioHang).Id,
                    IdComboChiTiet = _CB.GetAll().FirstOrDefault(c => c.Id == p.IdComboChiTiet).Id,
                };
                _context.gioHangChiTiets.Add(GH);
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
                var list = _context.gioHangChiTiets.ToList();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.gioHangChiTiets.Remove(obj);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Edit(Guid id, GioHangChiTiet p)
        {
            var listobj = _context.gioHangChiTiets.ToList();
            var obj = listobj.FirstOrDefault(c => c.Id == id);
            obj.SoLuong = p.SoLuong;
            obj.DonGia = p.DonGia;
            obj.IdSanPhamChiTiet = _SP.GetAll().FirstOrDefault(c => c.Id == p.IdSanPhamChiTiet).Id;
            obj.IdGioHang = _GH.GetAll().FirstOrDefault(c => c.Id == p.IdGioHang).Id;
            obj.IdComboChiTiet = _CB.GetAll().FirstOrDefault(c => c.Id == p.IdComboChiTiet).Id;
            _context.gioHangChiTiets.Update(obj);
            _context.SaveChangesAsync();
            return true;
        }
        public List<GioHangChiTiet> GetAll()
        {
            return _context.gioHangChiTiets.ToList();
        }
    }
}

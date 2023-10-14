using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{
    public class GioHangService : IGioHangService
    {
        private readonly MyDbContext _context;
        private readonly INguoiDungService _ND;
        public GioHangService()
        {
            _context = new MyDbContext();
            _ND = new NguoiDungService();
        }

        public bool Add(GioHang p)
        {
            try
            {

                GioHang GH = new GioHang()
                {
                    Id = Guid.NewGuid(),
                    GhiChu = p.GhiChu,
                    IdNguoiDung = _ND.GetAll().FirstOrDefault(c => c.Id == p.IdNguoiDung).Id,
                };
                _context.gioHangs.Add(GH);
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
                var list = _context.gioHangs.ToList();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.gioHangs.Remove(obj);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Edit(Guid id, GioHang p)
        {
            var listobj = _context.gioHangs.ToList();
            var obj = listobj.FirstOrDefault(c => c.Id == id);
            obj.GhiChu = p.GhiChu;
            obj.IdNguoiDung = _ND.GetAll().FirstOrDefault(c => c.Id == p.IdNguoiDung).Id;


            _context.gioHangs.Update(obj);
            _context.SaveChangesAsync();
            return true;
        }
        public List<GioHang> GetAll()
        {
            return _context.gioHangs.ToList();
        }
    }
}

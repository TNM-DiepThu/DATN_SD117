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
    public class ComBoChiTietService : IComboChiTietService
    {
        private readonly MyDbContext _context;
        private readonly IComboService comBoSer;
        public ComBoChiTietService()
        {
            _context = new MyDbContext();
            comBoSer = new ComboService();
        }

        public bool Add(ComboChiTiet p)
        {
            try
            {
                var comboCT = new ComboChiTiet()
                {
                    Id = Guid.NewGuid(),
                    SoLuongSanPham = p.SoLuongSanPham,
                    GiaBan = p.GiaBan,
                    IdCombo = comBoSer.GetAll().FirstOrDefault(c => c.Id == p.IdCombo).Id,

                };
                _context.comboChiTiets.Add(comboCT);
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
                var list = _context.comboChiTiets.ToList();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.comboChiTiets.Remove(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Edit(Guid id, ComboChiTiet p)
        {
            var listobj = _context.comboChiTiets.ToList();
            var obj = listobj.FirstOrDefault(c => c.Id == id);

            obj.SoLuongSanPham = p.SoLuongSanPham;
            obj.GiaBan = p.GiaBan;
            obj.IdCombo = comBoSer.GetAll().FirstOrDefault(c => c.Id == p.IdCombo).Id;
            _context.comboChiTiets.Update(obj);
            _context.SaveChanges();
            return true;
        }

        public List<ComboChiTiet> GetAll()
        {
            return _context.comboChiTiets.ToList();
        }
    }
}

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

        public string Add(ComboChiTiet p)
        {
            try
            {
                _context.comboChiTiets.Add(p);
                _context.SaveChanges();
                return "Thêm thành công";
            }
            catch (Exception)
            {
                return "Thêm thất bại";
            }
        }
        public bool Del(Guid id)
        {
            try
            {
                var obj = _context.comboChiTiets.FirstOrDefault(c => c.Id == id);
                obj.TrangThai = 0;
                _context.comboChiTiets.Update(obj);
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
            var listobj = _context.comboChiTiets.FirstOrDefault(c => c.Id == id);
            listobj = p;
            _context.comboChiTiets.Update(listobj);
            _context.SaveChanges();
            return true;
        }

        public List<ComboChiTiet> GetAll()
        {
            return _context.comboChiTiets.ToList();
        }

        public ComboChiTiet GetById(Guid id)
        {
            return _context.comboChiTiets.FirstOrDefault(c => c.Id == id);
        }
    }
}

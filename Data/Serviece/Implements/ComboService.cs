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
    public class ComboService : IComboService
    {
        private readonly MyDbContext _context;
        public ComboService()
        {
            _context = new MyDbContext();
        }

        public bool Add(Combo p)
        {
            try
            {
                _context.combos.Add(p);
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
                var list = _context.combos.ToList();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.combos.Remove(obj);
                _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Edit(Guid id, Combo p)
        { 
            var obj = _context.combos.FirstOrDefault(c => c.Id == id);
            obj =  p;
            _context.combos.Update(obj);
            _context.SaveChangesAsync();
            return true;
        }
        public List<Combo> GetAll()
        {
            return _context.combos.ToList();
        }
    }
}

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
    public class NguoiDungService : INguoiDungService
    {
        private MyDbContext _context;
        private Quyen quyen;
        public NguoiDungService()
        {
            _context = new MyDbContext();
            quyen = new Quyen();
        }

        public bool Add(NguoiDung user)
        {
            try
            {
                
                _context.Add(user);
                _context.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }

        public bool DeleteNguoiDung(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<NguoiDung> GetAll()
        {
            throw new NotImplementedException();
        }

        public NguoiDung GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public NguoiDung GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNguoiDung(Guid id, NguoiDung user)
        {
            throw new NotImplementedException();
        }
    }
}

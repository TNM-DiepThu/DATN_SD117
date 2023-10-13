using AppData.data;
using AppData.model;
using Bill.Serviece.Interfaces;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Implements
{
    public class SizeServiece : ISizeServiece
    {
        private readonly MyDbContext _context;
        public SizeServiece()
        {
            _context = new MyDbContext();
        }

        public bool Add(Size p)
        {
            try
            {
                Size size = new Size()
                {
                    Id = Guid.NewGuid(),
                    SizeName = p.SizeName,
                    status = 1,
                };
                _context.sizes.Add(size);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool Del(Guid id)
        {
            try
            {
                Size size = _context.sizes.FirstOrDefault(x => x.Id == id);
                if (size != null) 
                {
                    size.status = 0;
                };
                _context.sizes.Update(size);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, Size p)
        {
            try
            {
                Size size = _context.sizes.FirstOrDefault(x => x.Id == id);
                if (size != null)
                {
                    size.SizeName = p.SizeName;
                    size.status = p.status;
                };
                _context.sizes.Update(size);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Size> GetAll()
        {
            return _context.sizes.ToList();
        }
    }
}

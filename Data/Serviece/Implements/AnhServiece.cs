using AppData.data;
using AppData.model;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Implements
{
    public class AnhServiece : IAnhServiece
    {
        private readonly MyDbContext _context;
        public AnhServiece()
        {
            _context = new MyDbContext();

        }
        public bool Add(Anh p)
        {
            try
            {
                Anh anh = new Anh()
                {
                    Id = Guid.NewGuid(),
                    Connect = p.Connect,
                    status = p.status
                };
                _context.anhs.Add(anh);
                _context.SaveChanges(); 
                return true;

            }catch (Exception ex) 
            {
                return false;
            }
        }

        public bool Del(Guid id)
        {
            try
            {
                Anh anh = _context.anhs.FirstOrDefault(c => c.Id == id);
                if (anh != null)
                {
                    anh.status = 0;
                }
                _context.anhs.Update(anh);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, Anh p)
        {
            try
            {
                Anh anh = _context.anhs.FirstOrDefault(c => c.Id == id);
                if (anh != null)
                {
                    anh.status = p.status;
                    anh.Connect = p.Connect;
                }
                _context.anhs.Update(anh);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public List<Anh> GetAll()
        {
            return _context.anhs.ToList();
        }
    }
}

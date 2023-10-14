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
    public class ThuongHieuServiece : IThuongHieuServiece
    {
        private readonly MyDbContext _context;
        public ThuongHieuServiece()
        {
            _context = new MyDbContext();
        }

        public async Task<bool> Add(ThuongHieu p)
        {
            try
            {
                ThuongHieu thuonghieu = new ThuongHieu()
                {
                    Id = Guid.NewGuid(),
                    TenThuongHieu = p.TenThuongHieu,
                    Status = 1
                };
                _context.Add(thuonghieu);
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
                ThuongHieu thuonghieu = _context.thuongHieus.FirstOrDefault(c => c.Id == id);
                if(thuonghieu == null)
                {
                    thuonghieu.Status = 0;
                };
                _context.Update(thuonghieu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, ThuongHieu p)
        {
            try
            {
                ThuongHieu thuonghieu = _context.thuongHieus.FirstOrDefault(c => c.Id == id);
                if (thuonghieu == null)
                {
                    thuonghieu.TenThuongHieu = p.TenThuongHieu;
                    thuonghieu.Status = p.Status;
                };
                _context.Update(thuonghieu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ThuongHieu> GetAll()
        {
            return _context.thuongHieus.ToList();
        }
    }
}

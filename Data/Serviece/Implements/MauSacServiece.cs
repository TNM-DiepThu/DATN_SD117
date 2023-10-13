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
    public class MauSacServiece : IMauSacServiece
    {
        private readonly MyDbContext _context;
        public MauSacServiece()
        {
            _context = new MyDbContext();
        }

        public bool Add(MauSac p)
        {
            try
            {
                MauSac mauSac = new MauSac()
                {
                    Id = Guid.NewGuid(),
                    TenMauSac = p.TenMauSac,
                    status = 1
                };
                _context.Add(mauSac);
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
                MauSac mauSac = _context.mauSacs.FirstOrDefault(c => c.Id == id);
                if (mauSac != null) 
                {
                    mauSac.status = 0;
                };
                _context.mauSacs.Update(mauSac);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, MauSac p)
        {
            try
            {
                MauSac mauSac = _context.mauSacs.FirstOrDefault(c => c.Id == id);
                if (mauSac != null)
                {
                    mauSac.TenMauSac = p.TenMauSac;
                    mauSac.status = p.status;
                };
                _context.mauSacs.Update(mauSac);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<MauSac> GetAll()
        {
            return _context.mauSacs.ToList();
        }
    }
}


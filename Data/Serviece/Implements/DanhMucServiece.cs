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
    public class DanhMucServiece : IDanhMucServiece
    {
        private readonly MyDbContext _context;
        public DanhMucServiece()
        {
            _context = new MyDbContext();
        }

        public bool Add(DanhMuc p)
        {
            try
            {
                DanhMuc danhmuc = new DanhMuc()
                {
                    Id = Guid.NewGuid(),
                    TenDanhMuc = p.TenDanhMuc,
                    status = 1
                };
                _context.danhMucs.Add(danhmuc);
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
               DanhMuc danhmuc = _context.danhMucs.FirstOrDefault(c => c.Id == id);
                if (true)
                {
                    danhmuc.status = 0;
                }
                _context.danhMucs.Update(danhmuc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, DanhMuc p)
        {
            try
            {
                DanhMuc danhmuc = _context.danhMucs.FirstOrDefault(c => c.Id == id);
                if (true)
                {
                    danhmuc.TenDanhMuc = p.TenDanhMuc;
                    danhmuc.status = p.status;

                }
                _context.danhMucs.Update(danhmuc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<DanhMuc> GetAll()
        {
            return _context.danhMucs.ToList();
        }
    }
}

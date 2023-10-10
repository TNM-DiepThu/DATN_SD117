using AppData.data;
using AppData.model;
using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
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
        public async Task<bool> Add(DanhMucVM p)
        {
            try
            {
                var x = new DanhMuc()
                {
                    Id = Guid.NewGuid(),
                    TenDanhMuc = p.TenDanhMuc,
                    status = p.status
                };
                _context.Add(x);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Del(Guid id)
        {
            try
            {
                var list = await _context.danhMucs.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.danhMucs.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, DanhMucVM p)
        {
            try
            {
                var listobj = await _context.danhMucs.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.TenDanhMuc = p.TenDanhMuc;
                obj.status = p.status;

                _context.danhMucs.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<DanhMucVM>> GetAll()
        {
            var list = await _context.danhMucs.ToListAsync();
            var listvm = new List<DanhMucVM>();
            foreach (var item in list)
            {
                var x = new DanhMucVM();
                x.Id = item.Id;
                x.TenDanhMuc = item.TenDanhMuc;
                x.status = item.status;
                listvm.Add(x);
            }
            return listvm.ToList();
        }
    }
}

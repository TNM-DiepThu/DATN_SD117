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
    public class SizeServiece : ISizeServiece
    {
        private readonly MyDbContext _context;
        public SizeServiece()
        {
            _context = new MyDbContext();
        }
        public async Task<bool> Add(SizeVM p)
        {
            try
            {
                var anh = new Size()
                {
                    Id = Guid.NewGuid(),
                    SizeName = p.SizeName,
                    status = p.status
                };
                _context.Add(anh);
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
                var list = await _context.sizes.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.sizes.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, SizeVM p)
        {
            try
            {
                var listobj = await _context.sizes.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.SizeName = p.SizeName;
                obj.status = p.status;

                _context.sizes.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<SizeVM>> GetAll()
        {
            var list = await _context.sizes.ToListAsync();
            var listvm = new List<SizeVM>();
            foreach (var item in list)
            {
                var x = new SizeVM();
                x.Id = item.Id;
                x.SizeName = item.SizeName;
                x.status = item.status;
                listvm.Add(x);
            }
            return listvm.ToList();
        }
    }
}

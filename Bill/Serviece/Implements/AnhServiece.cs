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
    public class AnhServiece : IAnhServiece
    {
        private readonly MyDbContext _context;
        public AnhServiece()
        {
            _context = new MyDbContext();
        }
        public async Task<bool> Add(AnhVM p)
        {

            try
            {
                var anh = new Anh()
                {
                    Id = Guid.NewGuid(),
                    Connect = p.Connect,
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
                var list = await _context.anhs.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.anhs.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, AnhVM p)
        {
            try
            {
                var listobj = await _context.anhs.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.Connect = p.Connect;
                obj.status = p.status;
              
                _context.anhs.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<AnhVM>> GetAll()
        {
            var list = await _context.anhs.ToListAsync();
            var listvm = new List<AnhVM>();
            foreach (var item in list)
            {
                var x = new AnhVM();
                x.Id = item.Id;
                x.Connect = item.Connect;
                x.status = item.status;
                listvm.Add(x);
            }
            return listvm.ToList();
        }
    }
}

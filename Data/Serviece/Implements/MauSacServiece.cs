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
    public class MauSacServiece : IMauSacServiece
    {
        private readonly MyDbContext _context;
        public MauSacServiece()
        {
            _context = new MyDbContext();
        }
        public async Task<bool> Add(MauSacVM p)
        {
            try
            {
                var anh = new MauSac()
                {
                    Id = Guid.NewGuid(),
                    TenMauSac = p.TenMauSac,
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
                var list = await _context.mauSacs.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.mauSacs.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, MauSacVM p)
        {
            try
            {
                var listobj = await _context.mauSacs.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.TenMauSac = p.TenMauSac;
                obj.status = p.status;

                _context.mauSacs.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<MauSacVM>> GetAll()
        {
            var list = await _context.mauSacs.ToListAsync();
            var listvm = new List<MauSacVM>();
            foreach (var item in list)
            {
                var x = new MauSacVM();
                x.Id = item.Id;
                x.TenMauSac = item.TenMauSac;
                x.status = item.status;
                listvm.Add(x);
            }
            return listvm.ToList();
        }
    }
}


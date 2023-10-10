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
    public class ThuongHieuServiece : IThuongHieuServiece
    {
        private readonly MyDbContext _context;
        public ThuongHieuServiece()
        {
            _context = new MyDbContext();
        }

        public async Task<bool> Add(ThuongHieuVM p)
        {
            try
            {
                var thuonghieu = new ThuongHieu()
                {
                    Id = Guid.NewGuid(),
                    TenThuongHieu = p.TenThuongHieu,
                    Status = p.Status
                };
                _context.Add(thuonghieu);
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
                var list = await _context.thuongHieus.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.thuongHieus.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<bool> Edit(Guid id, ThuongHieuVM p)
        {
            try
            {
                var listobj = await _context.thuongHieus.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.TenThuongHieu = p.TenThuongHieu;
                obj.Status = p.Status;
                _context.thuongHieus.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<ThuongHieuVM>> GetAll()
        {
            var list = await _context.thuongHieus.ToListAsync();
            var listvm = new List<ThuongHieuVM>();
            foreach (var item in list)
            {
                var thuonghieu = new ThuongHieuVM();
                thuonghieu.Id = item.Id;
                thuonghieu.TenThuongHieu=item.TenThuongHieu;
                thuonghieu.Status = item.Status;
                listvm.Add(thuonghieu);
            }
            return listvm.ToList();
        }
    }
}

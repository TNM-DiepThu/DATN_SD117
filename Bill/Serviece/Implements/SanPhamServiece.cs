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
    public class SanPhamServiece : ISanPhamServiece
    {
        private readonly MyDbContext _context;
        public SanPhamServiece()
        {
            _context = new MyDbContext();
        }
        public async Task<bool> Add(SanPhamvm p)
        {
            try
            {
                var sp = new SanPham()
                {
                    Id = Guid.NewGuid(),
                    TenSanPham = p.TenSanPham,
                    status = p.status,
                    IdThuongHieu = p.IdThuongHieu,
                    IdXuatSu = p.IdXuatSu,
                };
                _context.Add(sp);
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
                var list = await _context.sanPhams.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.sanPhams.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, SanPhamvm p)
        {
            try
            {
                var listobj = await _context.sanPhams.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.TenSanPham = p.TenSanPham;
                obj.status = p.status;
                obj.IdThuongHieu = p.IdThuongHieu;
                obj.IdXuatSu = p.IdXuatSu;
                _context.sanPhams.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<SanPhamvm>> GetAll()
        {
            var list = await _context.sanPhams.ToListAsync();
            var listvm = new List<SanPhamvm>();
            foreach (var item in list)
            {
                var sp = new SanPhamvm();
                sp.Id = item.Id;
                sp.TenSanPham = item.TenSanPham;
                sp.status = item.status;
                sp.IdXuatSu = item.IdXuatSu;
                sp.IdThuongHieu = item.IdThuongHieu;
                listvm.Add(sp);
            }
            return listvm.ToList();
        }
    }
}

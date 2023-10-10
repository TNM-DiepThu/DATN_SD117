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
    public class SanPhamChiTietServiece : ISanPhamChiTietServiece
    {
        private readonly MyDbContext _context;
        public SanPhamChiTietServiece()
        {
            _context = new MyDbContext();
        }
        public async Task<bool> Add(ViewModal.SanPhamVM.SanPhamChiTietVM p)
        {
            try
            {
                var sp = new SanPhamChiTiet()
                {
                    Id = Guid.NewGuid(),
                    MaSp = p.MaSp,
                    status = p.status,
                    SoLuong = p.SoLuong,
                    GiaBan = p.GiaBan,
                    MoTa = p.MoTa,
                    IdAnh = p.IdAnh,
                    IdMauSac = p.IdMauSac,
                    IDSP = p.IDSP,
                    IdSize = p.IdSize,
                    IdChatLieu = p.IdChatLieu,
                    IdDanhMuc = p.IdDanhMuc,
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
                var list = await _context.sanPhamChiTiets.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.sanPhamChiTiets.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, ViewModal.SanPhamVM.SanPhamChiTietVM p)
        {
            try
            {
                var listobj = await _context.sanPhamChiTiets.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.MaSp = p.MaSp;
                obj.status = p.status;
                obj.SoLuong = p.SoLuong;
                obj.GiaBan = p.GiaBan;
                obj.MoTa = p.MoTa;
                obj.IdAnh = p.IdAnh;
                obj.IdMauSac = p.IdMauSac;
                obj.IDSP = p.IDSP;
                obj.IdSize = p.IdSize;
                obj.IdChatLieu = p.IdChatLieu;
                obj.IdDanhMuc = p.IdDanhMuc;
                _context.sanPhamChiTiets.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<ViewModal.SanPhamVM.SanPhamChiTietVM>> GetAll()
        {
            var list = await _context.sanPhamChiTiets.ToListAsync();
            var listvm = new List<SanPhamChiTietVM>();
            foreach (var item in list)
            {
                var sp = new SanPhamChiTietVM();
                sp.Id = item.Id;
                sp.MaSp = item.MaSp;
                sp.status = item.status;
                sp.SoLuong = item.SoLuong;
                sp.GiaBan = item.GiaBan;
                sp.MoTa = item.MoTa;
                sp.IdAnh = item.IdAnh;
                sp.IdMauSac = item.IdMauSac;
                sp.IDSP = item.IDSP;
                sp.IdSize   = item.IdSize;
                sp.IdChatLieu = item.IdChatLieu;
                sp.IdDanhMuc = item.IdDanhMuc;
                listvm.Add(sp);
            }
            return listvm.ToList();
        }
    }
}

using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.ThanhToanVM;
using Bill.ViewModal.SanPhamVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Implements
{
    public class HinhThucThanhToanServiece : IHinhThucThanhToanServices
    {
        private readonly MyDbContext _context;
        public HinhThucThanhToanServiece()
        {
            _context = new MyDbContext();
        }

        public async Task<bool> Add(HinhThucThanhToanVM p)
        {
            try
            {
                var httt = new HinhThucThanhToan()
                {
                    Id = Guid.NewGuid(),
                    TenHinhThucThanhToan = p.TenHTTT,
                    MoTa = p.MoTa,
                    status = p.TrangThai
                };
                _context.Add(httt);
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
                var list = await _context.hinhThucThanhToans.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.hinhThucThanhToans.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<bool> Edit(Guid id, HinhThucThanhToanVM p)
        {
            try
            {
                var listobj = await _context.hinhThucThanhToans.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.TenHinhThucThanhToan = p.TenHTTT;
                obj.MoTa = p.MoTa;
                obj.status = p.TrangThai;
                _context.hinhThucThanhToans.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<HinhThucThanhToanVM>> GetAll()
        {
            var list = await _context.hinhThucThanhToans.ToListAsync();
            var listvm = new List<HinhThucThanhToanVM>();
            foreach (var item in list)
            {
                var httt = new HinhThucThanhToanVM();
                httt.Id = item.Id;
                httt.TenHTTT = item.TenHinhThucThanhToan;
                httt.TrangThai = item.status;
                httt.MoTa = item.MoTa;
                listvm.Add(httt);
            }
            return listvm.ToList();
        }
    }
}

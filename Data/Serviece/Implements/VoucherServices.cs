using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.VoucherVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{
    public class VoucherServices : IVoucherServices
    {
        private readonly MyDbContext _context;
        public VoucherServices()
        {
            _context = new MyDbContext();
        }

        public async Task<bool> Add(VoucherVM v)
        {
            try
            {
                var vc = new Voucher()
                {
                    Id = Guid.NewGuid(),
                    MaVoucher = v.MaVoucher,
                    NgayTao = v.NgayTao,
                    NgayBatDau = v.NgayBatDau,
                    NgayKetThuc = v.NgayKetThuc,
                    GiaTriVoucher = v.GiaTriVoucher,
                    SoLuong =v.SoLuong,
                    MoTa = v.MoTa,
                    DieuKienGiamGia = v.DieuKienGiamGia,
                    status = v.TrangThai
                };
                _context.Add(vc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var list = await _context.voucher.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.voucher.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, VoucherVM v)
        {
            try
            {
                var listobj = await _context.voucher.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                    obj.MaVoucher = v.MaVoucher;
                    obj.NgayTao = v.NgayTao;
                    obj.NgayBatDau = v.NgayBatDau;
                    obj.NgayKetThuc = v.NgayKetThuc;
                    obj.GiaTriVoucher = v.GiaTriVoucher;
                    obj.SoLuong = v.SoLuong;
                    obj.MoTa = v.MoTa;
                    obj.DieuKienGiamGia = v.DieuKienGiamGia;
                    obj.status = v.TrangThai;
                _context.voucher.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<VoucherVM>> GetAll()
        {
            var list = await _context.voucher.ToListAsync();
            var listvm = new List<VoucherVM>();
            foreach (var v in list)
            {
                var obj = new VoucherVM();
                obj.Id = v.Id;
                obj.MaVoucher = v.MaVoucher;
                obj.NgayTao = v.NgayTao;
                obj.NgayBatDau = v.NgayBatDau;
                obj.NgayKetThuc = v.NgayKetThuc;
                obj.GiaTriVoucher = v.GiaTriVoucher;
                obj.SoLuong = v.SoLuong;
                obj.MoTa = v.MoTa;
                obj.DieuKienGiamGia = v.DieuKienGiamGia;
                obj.TrangThai = v.status;
                listvm.Add(obj);
            }
            return listvm.ToList();
        }
    }
}

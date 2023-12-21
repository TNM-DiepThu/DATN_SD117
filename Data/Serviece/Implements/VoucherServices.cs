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

        public async Task<bool> Add(VoucherViewModel v)
        {
            try
            {
                var vc = new Voucher()
                {
                    Id = Guid.NewGuid(),
                    MaVoucher = v.MaVoucher,
                    NgayTao = DateTime.Now,
                    NgayBatDau = v.NgayBatDau,
                    NgayKetThuc = v.NgayKetThuc,
                    GiaTriVoucher = v.GiaTriVoucher,
                    SoLuong = v.SoLuong,
                    Min = v.Min,
                    Max = v.Max,
                    MoTa = v.MoTa,
                    DieuKienGiamGia = v.DieuKienGiamGia,
                    status = 1
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

        public async Task<string> Delete(Guid id)
        {
            try
            {
                var list = await _context.voucher.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.voucher.Remove(obj);
                await _context.SaveChangesAsync();
                return "Xóa thành công.";
            }
            catch (Exception)
            {

                return "Xóa thất bại.";
            }
        }

        public async Task<bool> Edit(Guid id, VoucherViewModel v)
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
                obj.Min = v.Min;
                obj.Max = v.Max;
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

        public async Task<List<VoucherViewModel>> GetAll()
        {
            var list = await _context.voucher.ToListAsync();
            var listvm = new List<VoucherViewModel>();
            foreach (var v in list)
            {
                var obj = new VoucherViewModel();
                obj.Id = v.Id;
                obj.MaVoucher = v.MaVoucher;
                obj.NgayTao = v.NgayTao;
                obj.NgayBatDau = v.NgayBatDau;
                obj.NgayKetThuc = v.NgayKetThuc;
                obj.GiaTriVoucher = v.GiaTriVoucher;
                obj.SoLuong = v.SoLuong;
                obj.MoTa = v.MoTa;
                obj.Min = v.Min;
                obj.Max = v.Max;
                obj.DieuKienGiamGia = v.DieuKienGiamGia;
                obj.TrangThai = v.status;
                listvm.Add(obj);
            }
            return listvm.ToList();
        }
    }
}

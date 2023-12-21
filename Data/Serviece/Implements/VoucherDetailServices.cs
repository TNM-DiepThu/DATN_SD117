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
    public class VoucherDetailServices : IVoucherDetailServices
    {
        private readonly MyDbContext _context;
        private readonly  IVoucherServices _voucherService;
        public VoucherDetailServices()
        {
            _context = new MyDbContext();
            _voucherService = new VoucherServices();
        }

        public async Task<bool> Add(VoucherDetailVM v)
        {
            try
            {
                var vc = new VoucherDetail()
                {
                    Id = Guid.NewGuid(),
                    IdNguoiDung = _context.Users.FirstOrDefault(c => c.Id == v.Id_NguoiDung).Id,
                    IdVoucher = _voucherService.GetAll().Result.FirstOrDefault(c => c.Id == v.Id_Voucher).Id,
                    Soluong = v.SoLuong,
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
                var list = await _context.voucherDetail.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.voucherDetail.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, VoucherDetailVM v)
        {
            try
            {
                var listobj = await _context.voucherDetail.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.IdVoucher = v.Id_Voucher;
                obj.IdNguoiDung = v.Id_NguoiDung;
                obj.Soluong = v.SoLuong;
                obj.status = v.TrangThai;
                _context.voucherDetail.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<VoucherDetailVM>> GetAll()
        {
            var list = await _context.voucherDetail.ToListAsync();
            var listvm = new List<VoucherDetailVM>();
            foreach (var v in list)
            {
                var obj = new VoucherDetailVM();
                obj.Id = v.Id;
                obj.Id_Voucher = v.IdVoucher;
                obj.Id_NguoiDung = v.IdNguoiDung;
                obj.SoLuong = v.Soluong;
                obj.TrangThai = v.status;
                listvm.Add(obj);
            }
            return listvm.ToList();
        }
    }
}

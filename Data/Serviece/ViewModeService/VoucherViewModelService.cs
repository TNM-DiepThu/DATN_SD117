using AppData.data;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.VoucherVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.ViewModeService
{
    public class VoucherViewModelService
    {
        private readonly IVoucherDetailServices voucherDetailServices;
        private readonly MyDbContext _context;
        private readonly IVoucherServices voucherServices;
        public VoucherViewModelService()
        {
            voucherDetailServices = new VoucherDetailServices();
            voucherServices = new VoucherServices();
            _context = new MyDbContext();
        }
        public List<VoucherDetailHoanThien> GetListVoucherViewModelByIDNguoiDung(Guid idNguoiDung)
        {
            var lstvoucher = from a in  voucherDetailServices.GetAll().Result
                             join b in voucherServices.GetAll().Result on a.Id_Voucher equals b.Id
                             join c in _context.Users.ToList() on a.Id_NguoiDung equals c.Id
                             select new VoucherDetailHoanThien
                             {
                                 IDNguoiDuong = c.Id,
                                 IDVoucherDetail = b.Id,
                                 IDVoucher = c.Id,
                                 MaVoucher = b.MaVoucher,
                                 Min = b.Min,
                                 Max = b.Max,
                                 NgayBatDau = b.NgayBatDau,
                                 NgayKetThuc = b.NgayKetThuc,
                                 GiaTriVoucher = b.GiaTriVoucher,
                                 DieuKien = b.DieuKienGiamGia,
                                 Soluong = a.SoLuong,
                                 TrangThai = a.TrangThai,
                             };
            return lstvoucher.Where(c => c.IDNguoiDuong == idNguoiDung).ToList();
        }

        public List<VoucherDetailHoanThien> GetListVoucherViewModel()
        {
            var lstvoucher = from a in  voucherDetailServices.GetAll().Result
                             join b in  voucherServices.GetAll().Result on a.Id_Voucher equals b.Id
                             join c in _context.Users.ToList() on a.Id_NguoiDung equals c.Id
                             select new VoucherDetailHoanThien
                             {
                                 IDNguoiDuong = c.Id,
                                 IDVoucherDetail = b.Id,
                                 IDVoucher = c.Id,
                                 MaVoucher = b.MaVoucher,
                                 NgayBatDau = b.NgayBatDau,
                                 NgayKetThuc = b.NgayKetThuc,
                                 Min = b.Min,
                                 Max = b.Max,
                                 GiaTriVoucher = b.GiaTriVoucher,
                                 DieuKien = b.DieuKienGiamGia,
                                 Soluong = a.SoLuong,
                                 TrangThai = a.TrangThai,
                             };
            return lstvoucher.ToList();
        }

        public VoucherDetailHoanThien GetListVoucherViewModelById(Guid idvoucherDetail)
        {
            var lstvoucher = from a in voucherDetailServices.GetAll().Result
                             join b in voucherServices.GetAll().Result on a.Id_Voucher equals b.Id
                             join c in _context.Users.ToList() on a.Id_NguoiDung equals c.Id
                             select new VoucherDetailHoanThien
                             {
                                 IDNguoiDuong = c.Id,
                                 IDVoucherDetail = b.Id,
                                 IDVoucher = c.Id,
                                 MaVoucher = b.MaVoucher,
                                 NgayBatDau = b.NgayBatDau,
                                 NgayKetThuc = b.NgayKetThuc,
                                 GiaTriVoucher = b.GiaTriVoucher,
                                 Min = b.Min,
                                 Max = b.Max,
                                 DieuKien = b.DieuKienGiamGia,
                                 Soluong = a.SoLuong,
                                 TrangThai = a.TrangThai,
                             };
            return lstvoucher.FirstOrDefault(c => c.IDVoucherDetail == idvoucherDetail);
        }
        public VoucherDetailHoanThien GetListVoucherViewModelByName(string MaVoucher)
        {
            var lstvoucher = from a in voucherDetailServices.GetAll().Result
                             join b in voucherServices.GetAll().Result on a.Id_Voucher equals b.Id
                             join c in _context.Users.ToList() on a.Id_NguoiDung equals c.Id
                             select new VoucherDetailHoanThien
                             {
                                 IDNguoiDuong = c.Id,
                                 IDVoucherDetail = b.Id,
                                 IDVoucher = c.Id,
                                 MaVoucher = b.MaVoucher,
                                 NgayBatDau = b.NgayBatDau,
                                 Min = b.Min,
                                 Max = b.Max,
                                 NgayKetThuc = b.NgayKetThuc,
                                 GiaTriVoucher = b.GiaTriVoucher,
                                 DieuKien = b.DieuKienGiamGia,
                                 Soluong = a.SoLuong,
                                 TrangThai = a.TrangThai,
                             };
            return lstvoucher.FirstOrDefault(c => c.MaVoucher == MaVoucher);
        }
    }
}

﻿using AppData.data;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.GioHangChiTietViewModel;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.ViewModeService
{
    public class GioHangChiTietViewModelService
    {
        private readonly MyDbContext _context;
        private readonly IGioHangCTService _giohangctservice;
        private readonly SanPhamChiTietViewModelService _spctviewmodelservice;
        private readonly IGioHangService _giohangservice;
        private readonly ISanPhamChiTietServiece _sanphamctservice;
        private readonly ComBoChiTietViewModelService _comboctservice;
        public GioHangChiTietViewModelService()
        {
            _context = new MyDbContext();
            _giohangservice = new GioHangService();
            _spctviewmodelservice = new SanPhamChiTietViewModelService();
            _giohangctservice = new GioHangCTService();
            _sanphamctservice = new SanPhamChiTietServiece();
            _comboctservice = new ComBoChiTietViewModelService();
        }
        public IEnumerable<GioHangChiTietViewModel> GetAllListGioHang(Guid idNguoiDung)
        {
            List<GioHangChiTietViewModel> lst = new List<GioHangChiTietViewModel>();

            if (_giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(idNguoiDung).Any(c => c.IdComboChiTiet == null))
            {
                var ghct = _giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(idNguoiDung);

                IEnumerable<GioHangChiTietViewModel> lstspct = from a in _giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(idNguoiDung)
                                                               join b in _giohangservice.GetAll() on a.IdGioHang equals b.Id
                                                               join c in _spctviewmodelservice.GetWithOneImage() on a.IdSanPhamChiTiet equals c.Id
                                                               select new GioHangChiTietViewModel
                                                               {
                                                                   ID = a.Id,
                                                                   IDGH = b.Id,
                                                                   IDSanPham = _spctviewmodelservice.GetAll().FirstOrDefault(c => c.Id == a.IdSanPhamChiTiet).Id,
                                                                   TenSanPham = _spctviewmodelservice.GetAll().FirstOrDefault(c => c.Id == a.IdSanPhamChiTiet).TenSP,
                                                                   Mausac = c.MauSac,
                                                                   Size = c.Size,
                                                                   pathImage = c.Images != null ? c.Images[0].Connect : null,
                                                                   Soluong = a.SoLuong,
                                                                   GiaSanPham = a.DonGia,
                                                                   TienGiamGia = 0,
                                                               };
                foreach (var obj in lstspct)
                {
                    lst.Add(obj);
                }


            }
            if (_giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(idNguoiDung).Any(c => c.IdSanPhamChiTiet == null))
            {
                var comboct = _giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(idNguoiDung);
                IEnumerable<GioHangChiTietViewModel> lstCombo = from a in _giohangctservice.GetAllGioHangTheoNguoiDungDangNhap(idNguoiDung)
                                                                join b in _giohangservice.GetAll() on a.IdGioHang equals b.Id
                                                                join d in _comboctservice.GetAllComBoChiTiet() on a.IdComboChiTiet equals d.Id
                                                                select new GioHangChiTietViewModel
                                                                {
                                                                    ID = a.Id,
                                                                    IDGH = b.Id,
                                                                    IDSanPham = _comboctservice.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == a.IdComboChiTiet).Id,
                                                                    TenSanPham = _comboctservice.GetAllComBoChiTiet().FirstOrDefault(c => c.Id == a.IdComboChiTiet).TenComBo,
                                                                    Mausac = d.MauSac,
                                                                    Size = d.Size,
                                                                    pathImage = d.PathAnh,
                                                                    Soluong = a.SoLuong,
                                                                    GiaSanPham = d.ThanhTienComBo,
                                                                    TienGiamGia = d.TienGiamGia,
                                                                };
                foreach (var obj in lstCombo)
                {
                    lst.Add(obj);
                }
            }
            return lst;
        }
    }
}

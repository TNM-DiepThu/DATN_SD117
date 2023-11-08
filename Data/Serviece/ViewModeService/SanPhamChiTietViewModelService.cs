using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.SanPhamChiTietVM;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.ViewModeService
{
    public class SanPhamChiTietViewModelService
    {
        private readonly ISanPhamChiTietServiece _spct;
        private readonly ISanPhamServiece _sanpham;
        private readonly IMauSacServiece _mausac;
        private readonly IChatLieuServiece _chatlieu;
        private readonly IAnhSanPhamService _anhsanpham;
        private readonly MyDbContext _context;
        private readonly ISizeServiece _size;
        private readonly IDanhMucServiece _danhmuc;
        private readonly IAnhServiece _anh;
        public SanPhamChiTietViewModelService()
        {
            _anh = new AnhServiece();
            _spct = new SanPhamChiTietServiece();
            _context = new MyDbContext();
            _anhsanpham = new AnhSanPhamService();
            _sanpham = new SanPhamServiece();
            _mausac = new MauSacServiece();
            _chatlieu = new ChatLieuServiece();
            _size = new SizeServiece();
            _danhmuc = new DanhMucServiece();
        }
        public List<SanPhamChiTietViewModel> GetAll()
        {
            //List<SanPhamChiTietViewModel> lstsp = new List<SanPhamChiTietViewModel>();

            var spct = from a in _spct.GetAll()
                       join b in _sanpham.GetAll() on a.IDSP equals b.Id
                       join c in _mausac.GetAll() on a.IdMauSac equals c.Id
                       join d in _danhmuc.GetAll() on a.IdDanhMuc equals d.Id
                       join e in _size.GetAll() on a.IdSize equals e.Id
                       join h in _chatlieu.GetAll() on a.IdChatLieu equals h.Id
                       select new SanPhamChiTietViewModel
                       {
                           Id = a.Id,
                           DanhMuc = d.TenDanhMuc,
                           TenSP = b.TenSanPham,
                           ChatLieu = h.TenChatLieu,
                           MauSac = c.TenMauSac,
                           Size = e.SizeName,
                           MaSp = a.MaSp,
                           SoLuong = a.SoLuong,
                           GiaBan = a.GiaBan,
                           MoTa = a.MoTa,
                           status = a.status,
                       };
            var products = spct.ToList();
            foreach (var product in products)
            {
                // Lấy các hình ảnh cho sản phẩm
                var productImages = _context.anhSanPhams
                .Where(a => a.IdSanPhamChiTiet == product.Id)
                .Select(a => new Anh { Id = a.Idanh, Connect = a.Anh.Connect, status = a.Anh.status })
                .ToList();
                // Gán danh sách hình ảnh cho sản phẩm
                product.Images = productImages;
            }

            return products.ToList();
            //return spct.ToList();
        }
        public SanPhamChiTietViewModel GetById(Guid ID)
        {
            var spct = from a in _spct.GetAll()
                       join b in _sanpham.GetAll() on a.IDSP equals b.Id
                       join c in _mausac.GetAll() on a.IdMauSac equals c.Id
                       join d in _danhmuc.GetAll() on a.IdDanhMuc equals d.Id
                       join e in _size.GetAll() on a.IdSize equals e.Id
                       //join f in _anh.GetAll() on a.IdAnh equals f.Id
                       join h in _chatlieu.GetAll() on a.IdChatLieu equals h.Id
                       select new SanPhamChiTietViewModel
                       {
                           Id = a.Id,
                           DanhMuc = d.TenDanhMuc,
                           TenSP = b.TenSanPham,
                           ChatLieu = h.TenChatLieu,
                           MauSac = c.TenMauSac,
                           Size = e.SizeName,
                           MaSp = a.MaSp,
                           SoLuong = a.SoLuong,
                           GiaBan = a.GiaBan,
                           MoTa = a.MoTa,
                           status = a.status,
                       };
            var product = spct.FirstOrDefault(c => c.Id == ID);
            var productImages = _context.anhSanPhams
            .Where(a => a.IdSanPhamChiTiet == product.Id)
            .Select(a => new Anh { Id = a.Idanh, Connect = a.Anh.Connect, status = a.Anh.status })
            .ToList();
            // Gán danh sách hình ảnh cho sản phẩm
            product.Images = productImages;
            //}
            return product;
        }

        public IEnumerable<SanPhamChiTietViewModel> GetByName(string name)
        {
            var spct = from a in _spct.GetAll()
                       join b in _sanpham.GetAll() on a.IDSP equals b.Id
                       join c in _mausac.GetAll() on a.IdMauSac equals c.Id
                       join d in _danhmuc.GetAll() on a.IdDanhMuc equals d.Id
                       join e in _size.GetAll() on a.IdSize equals e.Id
                       //join f in _anh.GetAll() on a.IdAnh equals f.Id
                       join h in _chatlieu.GetAll() on a.IdChatLieu equals h.Id
                       select new SanPhamChiTietViewModel
                       {
                           Id = a.Id,
                           DanhMuc = d.TenDanhMuc,
                           TenSP = b.TenSanPham,
                           ChatLieu = h.TenChatLieu,
                           MauSac = c.TenMauSac,
                           Size = e.SizeName,
                           //Anh = f.Connect,
                           MaSp = a.MaSp,
                           SoLuong = a.SoLuong,
                           GiaBan = a.GiaBan,
                           MoTa = a.MoTa,
                           status = a.status,
                       };

            var products = spct.Where(c => c.TenSP.Contains(name)).OrderByDescending(c => c.MaSp).ToList();
            foreach (var product in products)
            {
                //Lấy các hình ảnh cho sản phẩm
                var productImages = _context.anhSanPhams
             .Where(a => a.IdSanPhamChiTiet == product.Id)
             .Select(a => new Anh { Id = a.Idanh, Connect = a.Anh.Connect, status = a.Anh.status })
             .ToList();
                // Gán danh sách hình ảnh cho sản phẩm
                product.Images = productImages;
            }

            return products;
            //return spct.Where(c => c.TenSP.Contains(name)).ToList();
        }
    }
}

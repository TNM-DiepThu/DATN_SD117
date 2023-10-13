using AppData.data;
using AppData.model;
using Bill.Serviece.Interfaces;

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
        private readonly IXuatSuServiece xuatxuservice;
        private readonly IThuongHieuServiece thuongHieuServiece;

        public SanPhamServiece()
        {
            _context = new MyDbContext();
            xuatxuservice = new XuatSuServiece();
            thuongHieuServiece = new ThuongHieuServiece();
        }

        public bool Add(SanPham p)
        {
            try
            {
                SanPham sanpham = new SanPham()
                {
                    Id = Guid.NewGuid(),
                    IdThuongHieu = thuongHieuServiece.GetAll().FirstOrDefault(c => c.Id == p.IdThuongHieu).Id,
                    IdXuatSu = xuatxuservice.GetAll().FirstOrDefault(c => c.Id == p.IdXuatSu).Id ,
                    TenSanPham = p.TenSanPham,
                    status = 1
                };
                _context.sanPhams.Add(sanpham);
                _context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool Del(Guid id)
        {
            try
            {
                SanPham sanpham =_context.sanPhams.FirstOrDefault(c => c.Id == id);
                if (sanpham != null) 
                {
                    sanpham.status = 0;
                };
                _context.sanPhams.Update(sanpham);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, SanPham p)
        {
            try
            {
                SanPham sanpham = _context.sanPhams.FirstOrDefault(c => c.Id == id);
                if (sanpham != null)
                {
                    sanpham.IdThuongHieu = xuatxuservice.GetAll().FirstOrDefault(c => c.Id == p.IdXuatSu).Id;
                    sanpham.IdXuatSu = thuongHieuServiece.GetAll().FirstOrDefault(c => c.Id == p.IdThuongHieu).Id;
                    sanpham.TenSanPham = p.TenSanPham;
                    sanpham.status = p.status;
                };
                _context.sanPhams.Update(sanpham);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<SanPham> GetAll()
        {
            return _context.sanPhams.ToList();
        }
    }
}

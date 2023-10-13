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
    public class SanPhamChiTietServiece : ISanPhamChiTietServiece
    {
        private readonly MyDbContext _context;
        private readonly IMauSacServiece mausacservice;
        private readonly IAnhServiece anhservice;
        private readonly ISanPhamServiece sanphamservice;
        private readonly ISizeServiece sizeservice;
        private readonly IChatLieuServiece chatLieuServiece;
        private readonly IDanhMucServiece danhmucservice;


        public SanPhamChiTietServiece()
        {
            _context = new MyDbContext();
            mausacservice = new MauSacServiece();
            anhservice = new AnhServiece();
            sanphamservice = new SanPhamServiece();
            sizeservice = new SizeServiece();
            chatLieuServiece = new ChatLieuServiece();
            danhmucservice = new DanhMucServiece();
        }

        public bool Add(SanPhamChiTiet p)
        {
            try
            {
                SanPhamChiTiet spct = new SanPhamChiTiet()
                {
                    Id = Guid.NewGuid(),
                    IdAnh = anhservice.GetAll().FirstOrDefault(c => c.Id == p.IdAnh).Id,
                    IdChatLieu = chatLieuServiece.GetAll().FirstOrDefault(c => c.Id == p.IdChatLieu).Id,
                    IdDanhMuc = danhmucservice.GetAll().FirstOrDefault(c => c.Id == p.IdDanhMuc).Id,
                    IdMauSac = mausacservice.GetAll().FirstOrDefault(c => c.Id == p.IdMauSac).Id,
                    IdSize = sizeservice.GetAll().FirstOrDefault(c => c.Id == p.IdSize).Id,
                    IDSP = sanphamservice.GetAll().FirstOrDefault(c => c.Id == p.IDSP).Id,
                    GiaBan = p.GiaBan,
                    SoLuong = p.SoLuong,
                    status = 1,
                    MaSp = p.MaSp,
                    MoTa = p.MoTa
                };
                _context.sanPhamChiTiets.Add(p);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool Del(Guid id)
        {
            try
            {
                SanPhamChiTiet spct =  _context.sanPhamChiTiets.FirstOrDefault(c => c.Id == id);
                if (spct != null) 
                {

                    spct.status = 0;
   
                };
                _context.sanPhamChiTiets.Update(spct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, SanPhamChiTiet p)
        {
            try
            {
                SanPhamChiTiet spct = _context.sanPhamChiTiets.FirstOrDefault(c => c.Id == id);
                if (spct == null) 
                {

                    spct.IdAnh = anhservice.GetAll().FirstOrDefault(c => c.Id == p.IdAnh).Id;
                    spct.IdChatLieu = chatLieuServiece.GetAll().FirstOrDefault(c => c.Id == p.IdChatLieu).Id;
                    spct.IdDanhMuc = danhmucservice.GetAll().FirstOrDefault(c => c.Id == p.IdDanhMuc).Id;
                    spct.IdMauSac = mausacservice.GetAll().FirstOrDefault(c => c.Id == p.IdMauSac).Id;
                    spct.IdSize = sizeservice.GetAll().FirstOrDefault(c => c.Id == p.IdSize).Id;
                    spct.IDSP = sanphamservice.GetAll().FirstOrDefault(c => c.Id == p.IDSP).Id;
                    spct.GiaBan = p.GiaBan;
                    spct.SoLuong = p.SoLuong;
                    spct.status = p.status;
                    spct.MaSp = p.MaSp;
                    spct.MoTa = p.MoTa;
                };
                _context.sanPhamChiTiets.Update(spct);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<SanPhamChiTiet> GetAll()
        {
            return _context.sanPhamChiTiets.ToList();
        }

        public SanPhamChiTiet GetByID(Guid id)
        {
            return _context.sanPhamChiTiets.FirstOrDefault(c => c.Id == id);
        }

        public SanPhamChiTiet GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}

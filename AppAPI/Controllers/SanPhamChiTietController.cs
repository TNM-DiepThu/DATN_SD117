using AppData.model;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamChiTietController : ControllerBase
    {
        private readonly ISanPhamChiTietServiece _sanphamCTsv;
        private readonly IAnhServiece _anhServiece;
        private readonly IDanhMucServiece _anhMuc;
        private readonly IChatLieuServiece _chatLieu;
        private readonly IMauSacServiece _auSacServiece;
        private readonly ISizeServiece sizeServiece;
        private readonly ISanPhamServiece sanPhamServiece;
        public SanPhamChiTietController()
        {
            _anhMuc = new DanhMucServiece();
            _anhServiece = new AnhServiece();
            _chatLieu = new ChatLieuServiece();
            _auSacServiece = new MauSacServiece();
            sizeServiece = new SizeServiece();
            sanPhamServiece = new SanPhamServiece();
            _sanphamCTsv = new SanPhamChiTietServiece();
        }
        [HttpGet("GetAll")]

        public IEnumerable<SanPhamChiTiet> GetAllAsync()
        {
            return _sanphamCTsv.GetAll();
        }
        [HttpGet("GetByID")]
        public SanPhamChiTiet GetByID(Guid Id)
        {
            return _sanphamCTsv.GetByID(Id);
        }

        [HttpPost("Create")]
        public bool CreateSanPhamChiTiet(Guid iddm , Guid idcl, Guid idms, Guid idsize, Guid idanh , Guid idsp, string masp, int soluong, decimal gia, string? mota)
        {
            SanPhamChiTiet spct = new SanPhamChiTiet()
            {
                Id = Guid.NewGuid(),
                IdAnh = _anhServiece.GetAll().FirstOrDefault(c => c.Id == idanh).Id,
                IdChatLieu = _chatLieu.GetAll().FirstOrDefault(c => c.Id == idcl).Id,
                IdDanhMuc = _anhMuc.GetAll().FirstOrDefault(c => c.Id == iddm).Id,
                IdMauSac = _auSacServiece.GetAll().FirstOrDefault(c => c.Id == idms).Id,
                IdSize = sizeServiece.GetAll().FirstOrDefault(c => c.Id == idsize).Id,
                IDSP = sanPhamServiece.GetAll().FirstOrDefault(c => c.Id == idsp).Id,
                MaSp = masp,
                SoLuong = soluong,
                GiaBan = gia,
                MoTa = mota,
                status = 1,
            };
            return _sanphamCTsv.Add(spct);
        }

        [HttpPut("Update")]
        public bool UpdateSanPhamChiTiet(Guid id , Guid iddm, Guid idcl, Guid idms, Guid idsize, Guid idanh, Guid idsp, string masp, int soluong, decimal gia, string? mota , int trangthai)
        {
            SanPhamChiTiet spct = _sanphamCTsv.GetAll().FirstOrDefault(c => c.Id == id);
            if(spct != null)
            {
                spct.IdAnh = _anhServiece.GetAll().FirstOrDefault(c => c.Id == idanh).Id;
                spct.IdChatLieu = _chatLieu.GetAll().FirstOrDefault(c => c.Id == idcl).Id;
                spct.IdDanhMuc = _anhMuc.GetAll().FirstOrDefault(c => c.Id == iddm).Id;
                spct.IdMauSac = _auSacServiece.GetAll().FirstOrDefault(c => c.Id == idms).Id;
                spct.IdSize = sizeServiece.GetAll().FirstOrDefault(c => c.Id == idsize).Id;
                spct.IDSP = sanPhamServiece.GetAll().FirstOrDefault(c => c.Id == idsp).Id;
                spct.MaSp = masp;
                spct.SoLuong = soluong;
                spct.GiaBan = gia;
                spct.MoTa = mota;
                spct.status = trangthai;
            }
       
            return _sanphamCTsv.Edit(id , spct);
        }
        [HttpPut("Delete/{Id}")]

        public bool DeleteSanPhamChiTiet(Guid Id)
        {
            SanPhamChiTiet spct = _sanphamCTsv.GetAll().FirstOrDefault(c => c.Id == Id);
            if(spct != null)
            {
                spct.status = 0;
            }
            return _sanphamCTsv.Edit(Id, spct);
        }


    }
}

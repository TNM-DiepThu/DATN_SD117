﻿using AppData.data;
using AppData.model;
using AppData.Serviece;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonCTController : ControllerBase
    {
        private readonly IHoaDonCTService<HoaDonChiTiet> _hoaDonCTService;
        private MyDbContext _dbContext = new MyDbContext();
        private DbSet<HoaDonChiTiet> hdct;

        public HoaDonCTController()
        {
            hdct = _dbContext.hoaDonChiTiets;
            HoaDonCTService<HoaDonChiTiet> all = new HoaDonCTService<HoaDonChiTiet>(_dbContext, hdct);
            _hoaDonCTService = all;
        }

        // GET: api/<HoaDonCTController>
        [HttpGet("get-hoadonct")]
        public IEnumerable<HoaDonChiTiet> GetHoaDonCT()
        {
            return _hoaDonCTService.GetAll();
        }


        // POST api/<HoaDonCTController>
        [HttpPost("create-hoadonct")]
        public bool CreateHoaDonCT(int soluong, decimal gia, int trangthai, Guid idhd, Guid idcomboct, Guid idspct)
        {
            var hdct = _hoaDonCTService.GetAll().FirstOrDefault(c => c.IDHD == idhd && c.IdSPCT == idspct);
            var spct = _dbContext.sanPhamChiTiets.FirstOrDefault(c => c.Id == idspct);
            if (hdct != null)
            {
                var newsl = hdct.SoLuong += soluong;
                if (newsl > spct.SoLuong)
                {
                    return false;
                }
                hdct.SoLuong = newsl;
                hdct.Gia += gia;
                hdct.status = trangthai;
                if (_hoaDonCTService.EditItem(hdct))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                HoaDonChiTiet hdcts = new HoaDonChiTiet();
                hdcts.Id = Guid.NewGuid();
                hdcts.SoLuong = soluong;
                hdcts.Gia = gia;
                hdcts.status = trangthai;
                hdcts.IDHD = idhd;
                hdcts.IdSPCT = idspct;
                hdcts.IdCombo = idcomboct;
                if (_hoaDonCTService.AddItem(hdcts))
                {
                    return true;
                }
                return false;
            }
        }

        // PUT api/<HoaDonCTController>/5
        [HttpPut("update-hoadonct")]
        public bool UpdateHoaDonCT(Guid id, int soluong, decimal gia, int trangthai, Guid idhd, Guid idcomboct, Guid idspct)
        {
            var hdct = _hoaDonCTService.GetAll().First(c => c.Id == id);
            hdct.SoLuong = soluong;
            hdct.Gia = gia;
            hdct.status = trangthai;
            hdct.IDHD = idhd;
            hdct.IdSPCT = idspct;
            hdct.IdCombo = idcomboct;
            if (_hoaDonCTService.EditItem(hdct))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<HoaDonCTController>/5
        [HttpDelete("delete-hoadonct")]
        public bool Delete(Guid id)
        {
            var hdct = _hoaDonCTService.GetAll().First(c => c.Id == id);
            if (_hoaDonCTService.RemoveItem(hdct))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

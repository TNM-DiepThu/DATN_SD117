﻿using AppData.data;
using AppData.model;
using AppData.Serviece;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using AppData.Serviece.ViewModeService;
using AppData.ViewModal.HoaDon;
using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonCTController : ControllerBase
    {
        private readonly IHoaDonCTService _hoaDonCTService;
        private MyDbContext _dbContext = new MyDbContext();
        private readonly ISanPhamChiTietServiece sanPhamChiTietServiece;
        private readonly IComboChiTietService comboChiTietService;
        private DbSet<HoaDonChiTiet> hdct;
        private readonly HoaDonChiTietViewModelService _hdctviewmodel;

        public HoaDonCTController()
        {
            hdct = _dbContext.hoaDonChiTiets;
            sanPhamChiTietServiece = new SanPhamChiTietServiece();
            comboChiTietService = new ComBoChiTietService();
            _hoaDonCTService = new HoaDonCTService();
            _hdctviewmodel = new HoaDonChiTietViewModelService();

        }
        [HttpGet("[action]")]
        public IEnumerable<HoaDonChiTiet> GetByID(Guid Id)
        {
            return _hoaDonCTService.GetAllByIdHd(Id);
        }
        [HttpGet("[action]")]
        public List<HoaDonCTViewModel> GetByHDCTByID(Guid Idhb)
        {
            return _hdctviewmodel.GetAllHoaDonChiTiet(Idhb);
        }

        // GET: api/<HoaDonCTController>

        // POST api/<HoaDonCTController>
        [HttpPost("[action]")]
        public bool CreateHoaDonCT(int soluong, decimal gia, int trangthai, Guid idhd, Guid? idcomboct, Guid? idspct)
        {
            HoaDonChiTiet hdcts = new HoaDonChiTiet();
            if (idcomboct == null)
            {
                hdcts.Id = Guid.NewGuid();
                hdcts.SoLuong = soluong;
                hdcts.Gia = gia;
                hdcts.status = trangthai;
                hdcts.IDHD = idhd;
                hdcts.IdCombo = null;
                hdcts.IdSPCT = idspct;
            } else if(idspct == null)
            {
                hdcts.Id = Guid.NewGuid();
                hdcts.SoLuong = soluong;
                hdcts.Gia = gia;
                hdcts.status = trangthai;
                hdcts.IDHD = idhd;
                hdcts.IdCombo = idcomboct;
                hdcts.IdSPCT = null;
            }       
            return _hoaDonCTService.AddItem(hdcts); 
        }

        // PUT api/<HoaDonCTController>/5
        [HttpPut("[action]")]
        public bool UpdateHoaDonCT(Guid id, int soluong, decimal gia, int trangthai, Guid idhd, Guid? idcomboct, Guid? idspct)
        {
            HoaDonChiTiet hdct = _hoaDonCTService.GetAllByIdHd(idhd).First(c => c.Id == id);
            if (idcomboct == null)
            {
                hdct.SoLuong = soluong;
                hdct.Gia = gia;
                hdct.status = trangthai;
                hdct.IDHD = idhd;
                hdct.IdCombo = null;
                hdct.IdSPCT = idspct;
            }
            else if (idspct == null)
            {
                hdct.SoLuong = soluong;
                hdct.Gia = gia;
                hdct.status = trangthai;
                hdct.IDHD = idhd;
                hdct.IdCombo = idcomboct;
                hdct.IdSPCT = null;
            }
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
        [HttpDelete("[action]")]
        public bool Delete(HoaDonChiTiet hdct)
        {
            var hdct1 = _hoaDonCTService.GetAllByIdHd(hdct.IDHD).First(c => c.Id == hdct.Id);
            if (_hoaDonCTService.RemoveItemById(hdct))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public bool HoaDonChiTietBanTaiQuay(Guid IDHoaDon, Guid? IdCombo, Guid? IDSpct, int Soluong)
        {
            HoaDonChiTiet hdct = new HoaDonChiTiet();
            hdct.Id = Guid.NewGuid();
            hdct.IDHD = IDHoaDon;
            if (IdCombo == null)
            {
                hdct.IdCombo = null;
                hdct.IdSPCT = IDSpct;
                hdct.Gia = sanPhamChiTietServiece.GetAll().FirstOrDefault(c => c.Id == IDSpct).GiaBan;
            }
            else if (IDSpct == null)
            {
                hdct.IdSPCT = null;
                hdct.IdCombo = IdCombo;
                hdct.Gia = comboChiTietService.GetAll().FirstOrDefault(c => c.Id == IdCombo).GiaBan;
            }
            hdct.SoLuong = Soluong;
            return true;
        }
    }
}

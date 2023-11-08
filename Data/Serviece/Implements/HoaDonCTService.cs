using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{

    public class HoaDonCTService : IHoaDonCTService
    {
        public MyDbContext _dbContext;

        public HoaDonCTService()
        {
            _dbContext = new MyDbContext();

        }

        public bool AddItem(HoaDonChiTiet hdct)
        {
            try
            {
                _dbContext.hoaDonChiTiets.Add(hdct);
                _dbContext.SaveChanges();
                return true;

            } catch
            {
                return false;
            }
        }

        public bool EditItem(HoaDonChiTiet hdct)
        {
            try
            {
                HoaDonChiTiet hoaDonChiTiet = _dbContext.hoaDonChiTiets.FirstOrDefault(c => c.Id == hdct.Id);
                if(hoaDonChiTiet == null)
                {
                    return false;
                } else
                {
                    _dbContext.hoaDonChiTiets.Update(hoaDonChiTiet);
                    _dbContext.SaveChanges();
                    return true;
                }
            } catch
            {
                return false;
            }
        }

        public IEnumerable<HoaDonChiTiet> GetAllByIdHd(Guid ID)
        {
            return _dbContext.hoaDonChiTiets.Where(c => c.IDHD == ID).ToList();
        }

        public bool RemoveItemById(HoaDonChiTiet hdct)
        {
            try
            {
                HoaDonChiTiet hoaDonChiTiet = _dbContext.hoaDonChiTiets.FirstOrDefault(c => c.Id == hdct.Id);
                if (hoaDonChiTiet != null)
                {
                    _dbContext.hoaDonChiTiets.Remove(hoaDonChiTiet);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            } catch
            {
                return false;
            }
           
        }
    }
}

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
    public class HoaDonService : IHoaDonService
    {
        public readonly MyDbContext _dbContext;

        public HoaDonService() 
        {
            _dbContext = new MyDbContext();

        }
        public string AddItem(HoaDon hd)
        {
            try
            {
                _dbContext.hoaDons.Add(hd);
                _dbContext.SaveChanges();
                return "Tạo hóa đơn thành công";
            }
            catch (Exception)
            {
                return "Tạo hóa đơn thất bại";
            }
        }

        public bool EditItem(HoaDon item)
        {
            try
            {
                _dbContext.Update(item);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<HoaDon> GetAll()
        {
            return _dbContext.hoaDons.ToList();
        }
        public List<HoaDon> GetAllByIDNguoiDung(Guid IDnguoidung)
        {
            return _dbContext.hoaDons.Where(c => c.IdNguoiDunh == IDnguoidung).ToList();
        }

        public HoaDon GetByID(Guid id)
        {
            return _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
        }

        public bool RemoveItem(HoaDon item)
        {
            try
            {
                _dbContext.Update(item);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateChoLayHang(Guid id)
        {
            try
            {
                var Hoadon = _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
                Hoadon.status = 2;
                _dbContext.hoaDons.Update(Hoadon);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e) 
            {
                return false;
            }
        }

        public bool UpdateDaLayHang(Guid id)
        {
            try
            {
                var Hoadon = _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
                Hoadon.status = 3;
                _dbContext.hoaDons.Update(Hoadon);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateDaThanhToan(Guid id)
        {
            try
            {
                var Hoadon = _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
                Hoadon.status = 4;
                _dbContext.hoaDons.Update(Hoadon);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateDaXacNhan(Guid id)
        {
            try
            {
                var Hoadon = _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
                Hoadon.status = 1;
                _dbContext.hoaDons.Update(Hoadon);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateHDCho(Guid id)
        {
            try
            {
                var Hoadon = _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
                Hoadon.status = 7;
                _dbContext.hoaDons.Update(Hoadon);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateDaNhanHang(Guid id)
        {
            try
            {
                var Hoadon = _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
                Hoadon.status = 6;
                _dbContext.hoaDons.Update(Hoadon);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateHuy(Guid id)
        {
            try
            {
                var Hoadon = _dbContext.hoaDons.FirstOrDefault(c => c.Id == id);
                Hoadon.status = 2;
                _dbContext.hoaDons.Update(Hoadon);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

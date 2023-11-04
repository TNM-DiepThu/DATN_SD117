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
        public bool AddItem(HoaDon hd)
        {
            try
            {
                _dbContext.hoaDons.Add(hd);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
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
    }
}

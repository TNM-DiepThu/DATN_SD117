using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{
    public class QuyenService : IQuyenService
    {
        private readonly MyDbContext _context;
        public QuyenService(MyDbContext context)
        {
            _context = context;
        }
        public bool AddQuyen(Quyen quyen)
        {
            try
            {
                var chucvu = new Quyen()
                {
                    Id = Guid.NewGuid(),
                    Name = quyen.Name,
                    status = quyen.status
                };
                _context.Add(chucvu);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
        public List<Quyen> GetAllQuyen()
        {
            try
            {
                List<Quyen> lstquyen = new List<Quyen>();
                return lstquyen.ToList();
            }
            catch (Exception ex)
            {
                return new List<Quyen>();
            }
        }

        public Quyen GetQuyenById(Guid id)
        {
            return null;
        }

        public bool RemoveQuyen(Guid id)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateQuyen(Quyen quyen)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

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
    public class XuatSuServiece : IXuatSuServiece
    {
        private readonly MyDbContext _context;
        public XuatSuServiece()
        {
            _context = new MyDbContext();
        }

        public bool Add(XuatSu p)
        {
            try
            {
                XuatSu xuatxu = new XuatSu()
                {
                    Id = Guid.NewGuid(),
                    TenXuatSu = p.TenXuatSu,
                    Status = 1
                };
                _context.xuatSus.Add(xuatxu);
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
                XuatSu xuatxu = _context.xuatSus.FirstOrDefault(c => c.Id == id);
                if (xuatxu == null) 
                {
                    xuatxu.Status = 0;
                };
                _context.xuatSus.Update(xuatxu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Edit(Guid id, XuatSu p)
        {
            try
            {
                XuatSu xuatxu = _context.xuatSus.FirstOrDefault(c => c.Id == id);
                if (xuatxu == null)
                {
                    xuatxu.TenXuatSu = p.TenXuatSu;
                    xuatxu.Status = p.Status;
                };
                _context.xuatSus.Update(xuatxu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<XuatSu> GetAll()
        {
            return _context.xuatSus.ToList();
        }
    }
}

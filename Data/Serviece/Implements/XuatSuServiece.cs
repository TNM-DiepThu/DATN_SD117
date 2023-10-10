using AppData.data;
using AppData.model;
using Bill.Serviece.Interfaces;
using Bill.ViewModal.SanPhamVM;
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
        public async Task<bool> Add(XuatSuVM p)
        {
            try
            {
                var xuatSu = new XuatSu()
                {
                    Id = Guid.NewGuid(),
                    TenXuatSu = p.TenXuatSu,
                    Status = p.Status
                };
                _context.Add(xuatSu);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Del(Guid id)
        {
            try
            {
                var list = await _context.xuatSus.ToListAsync();
                var obj = list.FirstOrDefault(c => c.Id == id);
                _context.xuatSus.Remove(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Edit(Guid id, XuatSuVM p)
        {
            try
            {
                var listobj = await _context.xuatSus.ToListAsync();
                var obj = listobj.FirstOrDefault(c => c.Id == id);
                obj.TenXuatSu = p.TenXuatSu;
                obj.Status = p.Status;
                _context.xuatSus.Update(obj);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<XuatSuVM>> GetAll()
        {
            var list = await _context.xuatSus.ToListAsync();
            var listvm = new List<XuatSuVM>();
            foreach (var item in list)
            {
                var xuatsu = new XuatSuVM();
                xuatsu.Id = item.Id;
                xuatsu.TenXuatSu = item.TenXuatSu;
                xuatsu.Status = item.Status;
                listvm.Add(xuatsu);
            }
            return listvm.ToList();
        }
    }
}

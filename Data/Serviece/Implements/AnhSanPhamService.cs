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
    public class AnhSanPhamService : IAnhSanPhamService
    {
        private readonly MyDbContext _context;
        public AnhSanPhamService()
        {
            _context = new MyDbContext();
        }
        public bool AddAnhChoSanPham(AnhSanPham anhSanPham)
        {
            try
            {
                _context.anhSanPhams.Add(anhSanPham);
                _context.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public List<AnhSanPham> GetAllAnhChoSanPham()
        {
            return _context.anhSanPhams.ToList();
        }

        public List<AnhSanPham> GetAllAnhChoSanPhamBySP(Guid idsanpham)
        {
            return _context.anhSanPhams.Where(c => c.IdSanPhamChiTiet == idsanpham).ToList();
        }
    }
}

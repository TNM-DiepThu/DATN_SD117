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
        public bool RemoveAnhSp(Guid idanh, Guid idsp)
        {
            try
            {
                AnhSanPham anhsanpham = _context.anhSanPhams.FirstOrDefault(c => c.Idanh == idanh && c.IdSanPhamChiTiet == idsp);
                _context.anhSanPhams.Remove(anhsanpham);
                _context.SaveChanges();
                return true;
            }  catch
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

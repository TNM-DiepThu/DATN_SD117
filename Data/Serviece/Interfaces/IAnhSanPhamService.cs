using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IAnhSanPhamService
    {
        public bool AddAnhChoSanPham(AnhSanPham anhSanPham) ;
        public List<AnhSanPham> GetAllAnhChoSanPham();
        public List<AnhSanPham> GetAllAnhChoSanPhamBySP(Guid idsanpham);
        public bool RemoveAnhSp(Guid idanh, Guid idsp);
    }
}

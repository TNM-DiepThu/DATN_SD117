using AppData.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface ISanPhamServiece
    {
        public bool Add(SanPham p);
        public List<SanPham> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, SanPham p);
    }
}

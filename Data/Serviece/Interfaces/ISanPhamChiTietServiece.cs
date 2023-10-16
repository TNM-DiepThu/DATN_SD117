using AppData.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface ISanPhamChiTietServiece
    {
        public string Add(SanPhamChiTiet p);
        public List<SanPhamChiTiet> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, SanPhamChiTiet p);
        public SanPhamChiTiet GetByID(Guid id);
        public SanPhamChiTiet GetByName(string name);
    }
}

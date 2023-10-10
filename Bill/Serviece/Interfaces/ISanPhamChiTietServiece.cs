using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface ISanPhamChiTietServiece
    {
        public Task<bool> Add(SanPhamChiTietVM p);
        public Task<List<SanPhamChiTietVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, SanPhamChiTietVM p);
    }
}

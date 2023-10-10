using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface ISanPhamServiece
    {
        public Task<bool> Add(SanPhamvm p);
        public Task<List<SanPhamvm>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, SanPhamvm p);
    }
}

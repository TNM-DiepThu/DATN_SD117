using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IDanhMucServiece
    {
        public Task<bool> Add(DanhMucVM p);
        public Task<List<DanhMucVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, DanhMucVM p);
    }
}

using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IThuongHieuServiece
    {
        public Task<bool> Add(ThuongHieuVM p);
        public Task<List<ThuongHieuVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id,ThuongHieuVM p);

    }
}

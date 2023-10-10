using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface ISizeServiece
    {
        public Task<bool> Add(SizeVM p);
        public Task<List<SizeVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, SizeVM p);
    }
}

using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IAnhServiece
    {
        public Task<bool> Add(AnhVM p);
        public Task<List<AnhVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, AnhVM p);
    }
}

using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IXuatSuServiece
    {
        public Task<bool> Add(XuatSuVM p);
        public Task<List<XuatSuVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, XuatSuVM p);
    }
}

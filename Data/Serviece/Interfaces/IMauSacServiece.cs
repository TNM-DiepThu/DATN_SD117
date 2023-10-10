using Bill.ViewModal.SanPhamVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IMauSacServiece
    {
        public Task<bool> Add(MauSacVM p);
        public Task<List<MauSacVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, MauSacVM p);
    }
}

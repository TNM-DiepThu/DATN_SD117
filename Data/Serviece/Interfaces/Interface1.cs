using AppData.ViewModal.ThanhToanVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface Interface1
    {

        public Task<bool> Add(HinhThucThanhToanVM v);
        public Task<bool> Edit(Guid id, HinhThucThanhToanVM v);
        public Task<bool> Delete(Guid id);
        public Task<List<HinhThucThanhToanVM>> GetAll();
        public Task<bool> GetById(Guid id);
    }
}

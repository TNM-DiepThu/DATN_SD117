using AppData.ViewModal.VoucherVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IVoucherServices
    {
        public Task<bool> Add(VoucherVM v);
        public Task<bool> Edit(Guid id,VoucherVM v);
        public Task<bool> Delete(Guid id);
        public Task<List<VoucherVM>> GetAll();
    }
}

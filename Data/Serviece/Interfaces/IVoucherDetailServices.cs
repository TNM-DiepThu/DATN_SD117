using AppData.ViewModal.VoucherVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IVoucherDetailServices
    {

        public Task<bool> Add(VoucherDetailVM v);
        public Task<bool> Edit(Guid id, VoucherDetailVM v);
        public Task<bool> Delete(Guid id);
        public Task<List<VoucherDetailVM>> GetAll();
    }
}

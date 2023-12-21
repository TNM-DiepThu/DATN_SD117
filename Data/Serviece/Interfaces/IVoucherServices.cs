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
        public Task<bool> Add(VoucherViewModel v);
        public Task<bool> Edit(Guid id,VoucherViewModel v);
        public Task<string> Delete(Guid id);
        public Task<List<VoucherViewModel>> GetAll();
    }
}

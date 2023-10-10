using AppData.ViewModal.ThanhToanVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IHinhThucThanhToanServices
    {
        public Task<bool> Add(HinhThucThanhToanVM p);
        public Task<List<HinhThucThanhToanVM>> GetAll();
        public Task<bool> Del(Guid id);
        public Task<bool> Edit(Guid id, HinhThucThanhToanVM p);
    }
}

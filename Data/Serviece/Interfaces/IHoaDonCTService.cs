using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    
        public interface IHoaDonCTService
        {
            public IEnumerable<HoaDonChiTiet> GetAllByIdHd(Guid ID);
            public bool AddItem(HoaDonChiTiet hdct);
            public bool RemoveItemById(HoaDonChiTiet hdct);
            public bool EditItem(HoaDonChiTiet hdct);
    }
    
}

using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IHoaDonService
    {
        public List<HoaDon> GetAll();
        public bool AddItem(HoaDon item);
        public bool RemoveItem(HoaDon item);
        public bool EditItem(HoaDon item);
        public HoaDon GetByID(Guid id);
    }
}

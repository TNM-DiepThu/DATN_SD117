using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IHoaDonService<T>
    {
        public IEnumerable<T> GetAll();
        public bool AddItem(T item);
        public bool RemoveItem(T item);
        public bool EditItem(T item);
        public HoaDon GetByID(Guid id);
    }
}

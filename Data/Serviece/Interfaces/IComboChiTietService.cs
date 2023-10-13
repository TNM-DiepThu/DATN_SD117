using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IComboChiTietService
    {
        public bool Add(ComboChiTiet p);
        public List<ComboChiTiet> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, ComboChiTiet p);
    }
}

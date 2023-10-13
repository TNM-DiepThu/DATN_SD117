using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IComboService
    {
        public bool Add(Combo p);
        public List<Combo> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, Combo p);

    }
}

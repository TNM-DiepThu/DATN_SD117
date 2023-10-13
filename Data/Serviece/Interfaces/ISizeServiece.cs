
using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface ISizeServiece
    {
        public bool Add(Size p);
        public List<Size> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, Size p);
    }
}

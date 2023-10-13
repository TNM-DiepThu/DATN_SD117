using AppData.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IXuatSuServiece
    {
        public bool Add(XuatSu p);
        public List<XuatSu> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, XuatSu p);
    }
}

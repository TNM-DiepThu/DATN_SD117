using AppData.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IDanhMucServiece
    {
        public bool Add(DanhMuc p);
        public List<DanhMuc> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, DanhMuc p);
    }
}

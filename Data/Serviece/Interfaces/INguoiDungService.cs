using AppData.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    internal interface INguoiDungService
    {
        public  bool Add(NguoiDung user);
        public List<NguoiDung> GetAll();
        public bool DeleteNguoiDung(Guid id);
        public bool UpdateNguoiDung(Guid id, NguoiDung user);
        public NguoiDung GetByName(string name);
        public NguoiDung GetById(Guid id);
    }
}

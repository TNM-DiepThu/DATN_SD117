using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IGioHangCTService
    {
        public bool Add(GioHangChiTiet p);
        public List<GioHangChiTiet> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, GioHangChiTiet p);
    }
}

using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IGioHangService
    {
        public bool Add(GioHang p);
        public List<GioHang> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, GioHang p);
    }
}

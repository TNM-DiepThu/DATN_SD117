using AppData.model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IThuongHieuServiece
    {
        public Task<bool> Add(ThuongHieu p);
        public List<ThuongHieu> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, ThuongHieu p);

    }
}

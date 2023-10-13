using AppData.model;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.Serviece.Interfaces
{
    public interface IMauSacServiece
    {
        public bool Add(MauSac p);
        public List<MauSac> GetAll();
        public bool Del(Guid id);
        public bool Edit(Guid id, MauSac p);
    }
}

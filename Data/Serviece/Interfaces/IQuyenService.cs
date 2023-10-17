using AppData.model;
using Microsoft.Azure.Cosmos.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IQuyenService
    {
        public bool AddQuyen(Quyen quyen);
        public bool RemoveQuyen(Guid id);
        public bool UpdateQuyen(Quyen quyen);  
        public List<Quyen> GetAllQuyen();
        public Quyen GetQuyenById(Guid id);
        public Task<List<Quyen>> GetAllPositionActive();
    }
}

using AppData.model;
using AppData.ViewModal.Usermodalview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface INguoiDungServiece
    {
        Task<NguoiDungVM> GetById(Guid id);
        Task<IEnumerable<NguoiDungVM>> GetAll();
        Task<Guid> Create(NguoiDungVM nguoiDung);
        Task Update(Guid id, NguoiDungVM nguoiDung);
        Task Delete(Guid id);
    }
}

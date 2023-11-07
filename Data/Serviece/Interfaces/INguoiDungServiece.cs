using AppData.model;
using AppData.ViewModal.Login;
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
        Task<IEnumerable<NguoiDungVM>> GetAllAsync();
        Task<NguoiDungVM> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(NguoiDungVM nguoiDung);
        Task<Guid> CreateNVAsync(NguoiDungVM nguoiDung);
        Task UpdateAsync(Guid id, NguoiDungEditVM nguoiDung);
        Task<bool>  DeleteAsync(Guid id);
        public Task<LoginResponesVM> LoginWithJWT(LoginRequestVM loginRequest);
        Task<IEnumerable<NguoiDungVM>> GetAllNV();
        Task<IEnumerable<NguoiDungVM>> GetAllKH();
        Task DoiMatKhau(Guid id, DoiMatKhauVM nguoiDung);
        Task<DoiMatKhauVM> GetByIdDMK(Guid id);

    }
}

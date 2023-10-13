﻿using AppData.model;
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
        Task UpdateAsync(Guid id, NguoiDungVM nguoiDung);
        Task DeleteAsync(Guid id);
    }
}
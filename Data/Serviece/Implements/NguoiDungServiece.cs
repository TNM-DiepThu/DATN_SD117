using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{
    public class NguoiDungServiece : INguoiDungServiece
    {
        private readonly UserManager<NguoiDung> _userManager;

        public NguoiDungServiece(UserManager<NguoiDung> userManager)
        {
            _userManager = userManager;
        }

        public async Task<NguoiDungVM> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return null;
            }

            var nguoiDungVM = new NguoiDungVM
            {
                SDT = user.PhoneNumber,
                MatKhau = "********", // Không nên trả về mật khẩu
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                NgaySinh = user.NgaySinh,
                status = user.status
            };

            return nguoiDungVM;
        }

        public async Task<IEnumerable<NguoiDungVM>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            var nguoiDungVMs = users.Select(user => new NguoiDungVM
            {
                SDT = user.PhoneNumber,
                MatKhau = "********",
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                NgaySinh = user.NgaySinh,
                status = user.status
            });

            return nguoiDungVMs;
        }

        public async Task<Guid> Create(NguoiDungVM nguoiDung)
        {
            var user = new NguoiDung
            {
                PhoneNumber = nguoiDung.SDT,
                QuanHuyen = nguoiDung.QuanHuyen,
                ThanhPho = nguoiDung.ThanhPho,
                DiaChi = nguoiDung.DiaChi,
                NgaySinh = nguoiDung.NgaySinh,
                status = nguoiDung.status
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return user.Id;
            }

            return Guid.Empty;
        }

        public async Task Update(Guid id, NguoiDungVM nguoiDung)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.PhoneNumber = nguoiDung.SDT;
                user.QuanHuyen = nguoiDung.QuanHuyen;
                user.ThanhPho = nguoiDung.ThanhPho;
                user.DiaChi = nguoiDung.DiaChi;
                user.NgaySinh = nguoiDung.NgaySinh;
                user.status = nguoiDung.status;

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}

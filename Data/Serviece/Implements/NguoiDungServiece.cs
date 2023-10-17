using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Implements
{
    public class NguoiDungServiece : INguoiDungServiece
    {
        private readonly UserManager<NguoiDung> _userManager;
        MyDbContext _dbContext;

        public NguoiDungServiece(UserManager<NguoiDung> userManager)
        {
            _userManager = userManager;
            _dbContext = new MyDbContext();
        }

        public async Task<IEnumerable<NguoiDungVM>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(user => new NguoiDungVM
            {
                id = user.Id,
                TenNguoiDung = user.UserName,
                Email = user.Email,
                SDT = user.SDT,
                MatKhau = user.MatKhau,
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                NgaySinh = user.NgaySinh,
                status = user.status
            });
        }

        public async Task<NguoiDungVM> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return null;

            return new NguoiDungVM
            {
                id = user.Id,
                Email = user.Email,
                SDT = user.SDT,
                TenNguoiDung = user.UserName,
                MatKhau = user.MatKhau,
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                NgaySinh = user.NgaySinh,
                status = user.status
            };
        }

        public async Task<Guid> CreateAsync(NguoiDungVM nguoiDung)
        {
            var user = new NguoiDung
            {
                Id = Guid.NewGuid(),
                UserName = nguoiDung.TenNguoiDung,
                Email = nguoiDung.Email,
                SDT = nguoiDung.SDT,
                MatKhau = nguoiDung.MatKhau,
                QuanHuyen = nguoiDung.QuanHuyen,
                ThanhPho = nguoiDung.ThanhPho,
                DiaChi = nguoiDung.DiaChi,
                NgaySinh = nguoiDung.NgaySinh,
                status = 0
            };

            var result = await _userManager.CreateAsync(user, nguoiDung.MatKhau);
            if (result.Succeeded)
            {
                await _dbContext.SaveChangesAsync();
                return user.Id;
            }
            else
            {
                var errors = result.Errors.Select(error => error.Description).ToArray();
                throw new Exception($"Failed to create user: {string.Join(", ", errors)}");
            }
        }

        public async Task UpdateAsync(Guid id, NguoiDungVM nguoiDung)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return;
            user.UserName = nguoiDung.TenNguoiDung;
            user.Email = nguoiDung.Email;
            user.SDT = nguoiDung.SDT;
            user.MatKhau = nguoiDung.MatKhau;
            user.QuanHuyen = nguoiDung.QuanHuyen;
            user.ThanhPho = nguoiDung.ThanhPho;
            user.DiaChi = nguoiDung.DiaChi;
            user.NgaySinh = nguoiDung.NgaySinh;
            user.status = nguoiDung.status;

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.status = 0;
            _dbContext.Users.Update(user);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}

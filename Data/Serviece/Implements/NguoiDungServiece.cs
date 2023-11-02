using AppData.data;
using AppData.model;
using AppData.Serviece.Interfaces;
using AppData.ViewModal.Login;
using AppData.ViewModal.Usermodalview;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data;
using Microsoft.AspNetCore.Hosting;


namespace AppData.Serviece.Implements
{
    public class NguoiDungServiece : INguoiDungServiece
    {
        private readonly SignInManager<NguoiDung> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<NguoiDung> _userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<Quyen> _roleManager;
        private readonly IConfiguration _configuration;
        MyDbContext _dbContext;

        public NguoiDungServiece(Microsoft.AspNetCore.Identity.UserManager<NguoiDung> userManager, SignInManager<NguoiDung> signInManager, IConfiguration configuration, Microsoft.AspNetCore.Identity.RoleManager<Quyen> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dbContext = new MyDbContext();
            _roleManager = roleManager;
        }


        public async Task<IEnumerable<NguoiDungVM>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(user => new NguoiDungVM
            {
                id = user.Id,
                Anh = user.Anh,
                TenNguoiDung = user.TenNguoiDung,
                username = user.UserName,             
                CCCD = user.CCCD,
                Email = user.Email,
                SDT = user.SDT,
                MatKhau = user.MatKhau,
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                GioiTinh = user.GioiTinh,
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
                username = user.UserName,
                TenNguoiDung = user.TenNguoiDung,
                Anh = user.Anh,
                CCCD = user.CCCD,
                Email = user.Email,
                SDT = user.SDT,
                MatKhau = user.MatKhau,
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                GioiTinh = user.GioiTinh,
                NgaySinh = user.NgaySinh,
                status = user.status
            };
        }
      
        public async Task<Guid> CreateAsync(NguoiDungVM nguoiDung)
        {


            var user = new NguoiDung
            {
                Id = Guid.NewGuid(),
                UserName = nguoiDung.username,
                TenNguoiDung = nguoiDung.TenNguoiDung,
                CCCD = nguoiDung.CCCD,
                Anh = "user.png",
                GioiTinh = nguoiDung.GioiTinh,
                Email = nguoiDung.Email,
                SDT = nguoiDung.SDT,
                MatKhau = nguoiDung.MatKhau,
                QuanHuyen = nguoiDung.QuanHuyen,
                ThanhPho = nguoiDung.ThanhPho,
                DiaChi = nguoiDung.DiaChi,
                NgaySinh = nguoiDung.NgaySinh,
                status = 1
            };

            var result = await _userManager.CreateAsync(user, nguoiDung.MatKhau);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("khachhang"))
                {
                    var role = new Quyen { Name = "khachhang" };
                    await _roleManager.CreateAsync(role);
                }
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "khachhang");

                if (addToRoleResult.Succeeded)
                {
                    await _dbContext.SaveChangesAsync();
                    return user.Id;
                }
                else
                {
                    var errors = addToRoleResult.Errors.Select(error => error.Description).ToArray();
                    throw new Exception($"Failed to add user to role: {string.Join(", ", errors)}");
                }
            }
            else
            {
                var errors = result.Errors.Select(error => error.Description).ToArray();
                throw new Exception($"Failed to create user: {string.Join(", ", errors)}");
            }
        }

        public async Task<Guid> CreateNVAsync(NguoiDungVM nguoiDung)
        {
            var user = new NguoiDung
            {
                Id = Guid.NewGuid(),
                UserName = nguoiDung.username,
                TenNguoiDung = nguoiDung.TenNguoiDung,
                CCCD = nguoiDung.CCCD,
                Anh = "user.png",
                GioiTinh = nguoiDung.GioiTinh,
                Email = nguoiDung.Email,
                SDT = nguoiDung.SDT,
                MatKhau = nguoiDung.MatKhau,
                QuanHuyen = nguoiDung.QuanHuyen,
                ThanhPho = nguoiDung.ThanhPho,
                DiaChi = nguoiDung.DiaChi,
                NgaySinh = Convert.ToDateTime(nguoiDung.NgaySinh),
                status = 1
            };

            var result = await _userManager.CreateAsync(user, nguoiDung.MatKhau);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("nhanvien"))
                {
                    var role = new Quyen { Name = "nhanvien" };
                    await _roleManager.CreateAsync(role);
                }
                var addToRoleResult = await _userManager.AddToRoleAsync(user, "nhanvien");

                if (addToRoleResult.Succeeded)
                {
                    await _dbContext.SaveChangesAsync();
                    return user.Id;
                }
                else
                {
                    var errors = addToRoleResult.Errors.Select(error => error.Description).ToArray();
                    throw new Exception($"Failed to add user to role: {string.Join(", ", errors)}");
                }
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
            user.UserName = nguoiDung.username; 
            user.TenNguoiDung = nguoiDung.TenNguoiDung;
            user.CCCD = nguoiDung.CCCD;
            user.Anh = nguoiDung.Anh;
            user.GioiTinh = nguoiDung.GioiTinh;
            user.Email = nguoiDung.Email;
            user.SDT = nguoiDung.SDT;
            user.MatKhau = nguoiDung.MatKhau;
            user.QuanHuyen = nguoiDung.QuanHuyen;
            user.ThanhPho = nguoiDung.ThanhPho;
            user.DiaChi = nguoiDung.DiaChi;
            user.NgaySinh = nguoiDung.NgaySinh;
            user.GioiTinh = nguoiDung.GioiTinh;
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

        public async Task<LoginResponesVM> LoginWithJWT(LoginRequestVM loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email) ?? await _userManager.FindByNameAsync(loginRequest.Username); 
            
            if (user == null) return new LoginResponesVM
            {
                Successful = false,
                Error = "Người dùng không tồn tại trong hệ thống."
            };
            var login = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, false);
            if (!login.Succeeded) return new LoginResponesVM
            {
                Successful = false,
                Error = "Người dùng không tồn tại trong hệ thống."
            };
            var rolesOfUser = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
            };
            foreach (var role in rolesOfUser)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            SecurityToken token = new JwtSecurityToken(
              _configuration["Jwt:Issuer"],
              _configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddHours(3),
              signingCredentials: creds);
            if (token is not JwtSecurityToken jwtSecurityToken ||
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return new LoginResponesVM
                {
                    Successful = false,
                    Error = "Refresh token không hợp lệ."
                };
            }
            LoginResponesVM loginResponesVm = new LoginResponesVM { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) };

            return loginResponesVm;
        }

        public async Task<IEnumerable<NguoiDungVM>> GetAllNV()
        {
            var users = await _userManager.GetUsersInRoleAsync("NhanVien");
            return users.Select(user => new NguoiDungVM
            {
                id = user.Id,
                username = user.UserName,
                TenNguoiDung = user.TenNguoiDung,
                Anh = user.Anh,
                CCCD = user.CCCD,
                Email = user.Email,
                SDT = user.SDT,
                MatKhau = user.MatKhau,
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                GioiTinh = user.GioiTinh,
                NgaySinh = user.NgaySinh,
                status = user.status
            });
        }

        public async Task<IEnumerable<NguoiDungVM>> GetAllKH()
        {
            var users = await _userManager.GetUsersInRoleAsync("KhachHang");
            return users.Select(user => new NguoiDungVM
            {
                id = user.Id,
                username = user.UserName,
                TenNguoiDung = user.TenNguoiDung,
                Anh = user.Anh,
                CCCD = user.CCCD,
                Email = user.Email,
                SDT = user.SDT,
                MatKhau = user.MatKhau,
                QuanHuyen = user.QuanHuyen,
                ThanhPho = user.ThanhPho,
                DiaChi = user.DiaChi,
                GioiTinh = user.GioiTinh,
                NgaySinh = user.NgaySinh,
                status = user.status
            });
        }
    }
}

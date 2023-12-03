using AppData.model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppData.data
{
    public class MyDbContext : IdentityDbContext<NguoiDung, Quyen, Guid>
    {
        public MyDbContext()
        {

        }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Anh> anhs { get; set; }
        public DbSet<AnhSanPham> anhSanPhams { get;set; }
        public DbSet<BinhLuan> binhLuans { get; set; }
        public DbSet<ChatLieu> chatLieus { get; set; }
        public DbSet<Combo> combos { get; set; }
        public DbSet<ComboChiTiet> comboChiTiets { get; set; }
        public DbSet<DanhMuc> danhMucs { get; set; }
        public DbSet<GioHang> gioHangs { get; set; }
        public DbSet<GioHangChiTiet> gioHangChiTiets { get; set; }
        public DbSet<HinhThucThanhToan> hinhThucThanhToans { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public DbSet<LichSuHoaDon> lichSuHoas { get; set; }
        public DbSet<MauSac> mauSacs { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<SanPhamChiTiet> sanPhamChiTiets { get; set; }
        public DbSet<Size> sizes { get; set; }
        public DbSet<ThuongHieu> thuongHieus { get; set; }
        public DbSet<XuatSu> xuatSus { get; set; }
        public DbSet<Voucher> voucher { get; set; }
        public DbSet<VoucherDetail> voucherDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-L9J8TJS;Initial Catalog=lzlzlzl;Integrated Security=True");
                optionsBuilder.UseSqlServer("Server=WINDOWS10\\SQLEXPRESS;Database=TestNew;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
        
    }
}

using AppData.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configuration
{
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MaHD).HasColumnType("nvarchar(100)");
            builder.Property(x => x.NgayTao).HasColumnType("DateTime");
            builder.Property(x => x.NgayGiao).HasColumnType("DateTime");
            builder.Property(x => x.NgayNhan).HasColumnType("DateTime");
            builder.Property(x => x.NgayThanhToan).HasColumnType("DateTime");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.TongTien).HasColumnType("decimal");
            builder.Property(x => x.TienVanChuyen).HasColumnType("decimal");
            builder.Property(x => x.NguoiNhan).HasColumnType("nvarchar(100)");
            builder.Property(x => x.SDT).HasColumnType("nvarchar(100)");
            builder.Property(x => x.QuanHuyen).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Tinh).HasColumnType("nvarchar(100)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(100)");
            builder.Property(x => x.GhiChu).HasColumnType("nvarchar(100)");
            builder.Property(x => x.status).HasColumnType("int");
            builder.HasOne(x => x.NguoiDung).WithMany(x => x.hoaDons).HasForeignKey(x => x.IdNguoiDunh).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(x => x.voucherDetail).WithMany(x => x.hoaDons).HasForeignKey(x => x.IdVoucherDetail).OnDelete(DeleteBehavior.Restrict).IsRequired(false); 
            builder.HasOne(x => x.HinhThucThanhToan).WithMany(x => x.HoaDons).HasForeignKey(x => x.IDHTTT).OnDelete(DeleteBehavior.Restrict); ;
        }
    }
}

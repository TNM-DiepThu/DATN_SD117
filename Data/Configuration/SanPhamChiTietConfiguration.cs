
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
    public class SanPhamChiTietConfiguration : IEntityTypeConfiguration<SanPhamChiTiet>
    {
        public void Configure(EntityTypeBuilder<SanPhamChiTiet> builder)
        {
            builder.HasKey (x => x.Id);
            builder.Property(x => x.MaSp).HasColumnType("nvarchar(100)");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.GiaBan).HasColumnType("decimal"); 
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(100)");
            builder.Property(x => x.status).HasColumnType("int");
            builder.HasOne(x => x.SanPham).WithMany(x => x.SanPhamChiTiets).HasForeignKey(x => x.IDSP);
            builder.HasOne(x => x.Anh).WithMany(x => x.sanPhamChiTiets).HasForeignKey(x => x.IdAnh);
            builder.HasOne(x => x.MauSac).WithMany(x => x.phamChiTiet).HasForeignKey(x => x.IdMauSac);
            builder.HasOne(x => x.Size).WithMany(x => x.sanPhamChiTiets).HasForeignKey(x => x.IdSize);
            builder.HasOne(x => x.ChatLieu).WithMany(x => x.sanPhamChiTiets).HasForeignKey(x => x.IdChatLieu);
            builder.HasOne(x => x.DanhMuc).WithMany(x => x.phamChiTiet).HasForeignKey(x => x.IdDanhMuc);
        }
    }
}

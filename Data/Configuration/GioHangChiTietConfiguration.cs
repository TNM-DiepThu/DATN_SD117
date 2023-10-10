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
    public class GioHangChiTietConfiguration : IEntityTypeConfiguration<GioHangChiTiet>
    {
        public void Configure(EntityTypeBuilder<GioHangChiTiet> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x=>x.SoLuong).HasColumnType("int");
            builder.Property(x=>x.DonGia).HasColumnType("decimal");
            builder.HasOne(x=>x.GioHang).WithMany(x=>x.gioHangChiTiets).HasForeignKey(x=>x.IdGioHang).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(x=>x.ComboChiTiet).WithMany(x=>x.GioHangChiTiets).HasForeignKey(x=>x.IdComboChiTiet).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(x=>x.SanPhamChiTiet).WithMany(x=>x.gioHangChiTiets).HasForeignKey(x=>x.IdSanPhamChiTiet).OnDelete(DeleteBehavior.Restrict); ;
        }
    }
}

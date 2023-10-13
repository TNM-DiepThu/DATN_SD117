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
    public class HoaDonChiTietConfiguration : IEntityTypeConfiguration<HoaDonChiTiet>
    {
        public void Configure(EntityTypeBuilder<HoaDonChiTiet> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.status).HasColumnType("int");
            builder.Property(x => x.Gia).HasColumnType("decimal");
            builder.HasOne(x => x.HoaDon).WithMany(x => x.hoaDonChiTiets).HasForeignKey(x => x.IDHD).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.comboChiTiet).WithMany(x => x.hoaDonChiTiets).HasForeignKey(x => x.IdCombo).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SanPhamChiTiet).WithMany(x => x.hoaDonChiTiets).HasForeignKey(x => x.IdSPCT).OnDelete(DeleteBehavior.Restrict);

        }
    }
}

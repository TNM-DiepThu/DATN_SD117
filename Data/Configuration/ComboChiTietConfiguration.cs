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
    public class ComboChiTietConfiguration : IEntityTypeConfiguration<ComboChiTiet>
    {
        public void Configure(EntityTypeBuilder<ComboChiTiet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SoLuongSanPham).HasColumnType("int");
            builder.Property(x => x.GiaBan).HasColumnType("decimal");
            builder.Property(x => x.GiaGoc).HasColumnType("decimal");
            builder.Property(x => x.TienGiamGia).HasColumnType("decimal");
            builder.Property(x => x.TrangThai).HasColumnType("int");
            builder.Property(x => x.SoLuongCombo).HasColumnType("int");
            builder.Property(x => x.TenCombo).HasColumnType("nvarchar(1000)");
            builder.HasOne(x => x.combos).WithMany(x => x.ComboChiTiet).HasForeignKey(x => x.IdCombo).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.SanPhamChiTiet).WithMany(x => x.comboChiTiets).HasForeignKey(x => x.IdSPCT).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

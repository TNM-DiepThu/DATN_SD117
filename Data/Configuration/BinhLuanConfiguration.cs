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
    public class BinhLuanConfiguration : IEntityTypeConfiguration<BinhLuan>
    {
        public void Configure(EntityTypeBuilder<BinhLuan> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.NgayTao).HasColumnType("DateTime");
            builder.Property(x=>x.DanhGiaSanPham).HasColumnType("nvarchar(100)");
            builder.Property(x=>x.NoiDung).HasColumnType("nvarchar(100)");
            builder.Property(x=>x.HinhAnh).HasColumnType("nvarchar(100)");
            builder.Property(x=>x.status).HasColumnType("int");
            builder.HasOne(x => x.SanPhamChiTiets).WithMany(x => x.BinhLuans).HasForeignKey(x => x.IdSanPhamChiTiet).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.nguoiDung).WithMany(x => x.BinhLuans).HasForeignKey(x => x.IdNguoiDung).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

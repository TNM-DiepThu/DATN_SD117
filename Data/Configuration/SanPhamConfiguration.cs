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
    public class SanPhamConfiguration : IEntityTypeConfiguration<SanPham>

    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPham");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.TenSanPham).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.status).HasColumnType("int");
            builder.HasOne(x=>x.XuatSu).WithMany(x=>x.SanPham).HasForeignKey(x=>x.IdXuatSu);
            builder.HasOne(x=>x.ThuongHieu).WithMany(x=>x.SanPham).HasForeignKey(x=>x.IdThuongHieu);
        }
    }
}

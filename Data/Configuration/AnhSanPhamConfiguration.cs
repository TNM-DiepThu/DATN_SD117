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
    public class AnhSanPhamConfiguration : IEntityTypeConfiguration<AnhSanPham>
    {
        public void Configure(EntityTypeBuilder<AnhSanPham> builder)
        {
            builder.HasKey(p => new { p.Idanh, p.IdSanPhamChiTiet });
            builder.HasOne(p => p.SanPhamChiTiet).WithMany(c => c.Anhlist).HasForeignKey(c => c.IdSanPhamChiTiet);
            builder.HasOne(c => c.Anh).WithMany().HasForeignKey(c => c.Idanh);
        }
    }
}

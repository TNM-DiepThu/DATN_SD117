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
    public class LichSuHoaDonConfiguration : IEntityTypeConfiguration<LichSuHoaDon>
    {
        public void Configure(EntityTypeBuilder<LichSuHoaDon> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x => x.NgayTao).HasColumnType("datetime");
            builder.Property(x => x.GhiChu).HasColumnType("nvarchar(100)");
            builder.Property(x => x.NguoiTao).HasColumnType("nvarchar(100)");
            builder.HasOne(x => x.nguoiDung).WithMany(x => x.lichSuHoaDons).HasForeignKey(x => x.IdNguoiDung);
            builder.HasOne(x => x.HoaDon).WithMany(x => x.LichSuHoaDons).HasForeignKey(x => x.IdHoaDon).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

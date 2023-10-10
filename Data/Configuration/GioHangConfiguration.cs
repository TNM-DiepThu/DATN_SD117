using AppData.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configuration
{
    public class GioHangConfiguration : IEntityTypeConfiguration<GioHang>
    {
        public void Configure(EntityTypeBuilder<GioHang> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.GhiChu).HasColumnType("nvarchar(100)");
            builder.HasOne(x=>x.nguoiDung).WithMany(x=>x.gioHangs).HasForeignKey(x=>x.IdNguoiDung).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

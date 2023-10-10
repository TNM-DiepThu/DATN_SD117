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
    public class HinhThucThanhToanConfiguration : IEntityTypeConfiguration<HinhThucThanhToan>
    {
        public void Configure(EntityTypeBuilder<HinhThucThanhToan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.TenHinhThucThanhToan).HasColumnType("nvarchar(100)");
            builder.Property(x=>x.MoTa).HasColumnType("nvarchar(100)");
            builder.Property(x=>x.status).HasColumnType("int");

        }
    }
}

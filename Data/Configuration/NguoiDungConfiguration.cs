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
    public class NguoiDungConfiguration : IEntityTypeConfiguration<NguoiDung>
    {
        public void Configure(EntityTypeBuilder<NguoiDung> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SDT).HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(x => x.TenNguoiDung).HasColumnType("nvarchar(100)");
            builder.Property(x => x.CCCD).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Anh).HasColumnType("nvarchar(100)").IsRequired(false);
            builder.Property(x => x.GioiTinh).HasColumnType("int");
            builder.Property(x => x.MatKhau).HasColumnType("nvarchar(100)");
            builder.Property(x => x.QuanHuyen).HasColumnType("nvarchar(100)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(100)");
            builder.Property(x => x.ThanhPho).HasColumnType("nvarchar(100)");
            builder.Property(x => x.status).HasColumnType("int");
            builder.Property(x => x.NgaySinh).HasColumnType("DateTime");
            
        }
    }
}

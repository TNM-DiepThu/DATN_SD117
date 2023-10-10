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
    public class DanhMucConfiguration : IEntityTypeConfiguration<DanhMuc>
    {
        public void Configure(EntityTypeBuilder<DanhMuc> builder)
        {
            builder.ToTable("DanhMuc");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.TenDanhMuc).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.status).HasColumnType("int");

        }
    }
}

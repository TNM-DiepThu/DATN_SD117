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
    public class XuatSuConfigiration : IEntityTypeConfiguration<XuatSu>
    {
        public void Configure(EntityTypeBuilder<XuatSu> builder)
        {
            builder.ToTable("XuatSu"); 
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.TenXuatSu).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Status).HasColumnType("int");

        }
    }
}

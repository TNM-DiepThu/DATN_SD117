using AppData.model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configuration
{
        public class MauConfiguration : IEntityTypeConfiguration<MauSac>
        {

            public void Configure(EntityTypeBuilder<MauSac> builder)
            {
                builder.ToTable("MauSac");
                builder.HasKey(x => x.Id);
                builder.Property(x => x.TenMauSac).HasColumnType("nvarchar(100)").IsRequired();
                builder.Property(x => x.status).HasColumnType("int");
            }
        }   
}

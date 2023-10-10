using AppData.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppData.Configuration
{
    public class AnhConfiguration:IEntityTypeConfiguration<Anh>
    {

        public void Configure(EntityTypeBuilder<Anh> builder)
        {
            builder.ToTable("Anh");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Connect).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x=>x.status).HasColumnType("int");
        }
    }
}

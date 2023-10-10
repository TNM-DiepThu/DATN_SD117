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
    internal class QuyenConfiguration : IEntityTypeConfiguration<Quyen>
    {
        public void Configure(EntityTypeBuilder<Quyen> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasColumnType("nvarchar(256)").IsRequired();
            builder.Property(x => x.status).HasColumnType("int");
        }
    }
}

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
    public class ComboConfiguration : IEntityTypeConfiguration<Combo>
    {
        public void Configure(EntityTypeBuilder<Combo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.TenCombo).HasColumnType("nvarchar(100)");
            builder.Property(x=>x.MoTaCombo).HasColumnType("nvarchar(100)");
            builder.Property(x=>x.TienGiamGia).HasColumnType("decimal");
            builder.Property(x=>x.status).HasColumnType("int");
        }
    }
}

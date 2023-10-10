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
    public class ChatLieuConfiguration : IEntityTypeConfiguration<ChatLieu>
    {
        public void Configure(EntityTypeBuilder<ChatLieu> builder)
        {
            builder.ToTable("ChatLieu");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TenChatLieu).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.status).HasColumnType("int");
        }
    }
}

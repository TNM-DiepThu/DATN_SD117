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
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MaVoucher).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.NgayTao).HasColumnType("DateTime");
            builder.Property(x => x.NgayBatDau).HasColumnType("DateTime");
            builder.Property(x => x.NgayKetThuc).HasColumnType("DateTime");
            builder.Property(x => x.GiaTriVouchet).HasColumnType("Decimal");
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.Property(x => x.MoTa).HasColumnType("nvarchar(200)"); ;
            builder.Property(x => x.DieuKienGiamGia).HasColumnType("nvarchar(200)");
            builder.Property(x => x.status).HasColumnType("int");
        }
    }
}

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
    public class VoucherDetailConfiguration : IEntityTypeConfiguration<VoucherDetail>
    {
        public void Configure(EntityTypeBuilder<VoucherDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Soluong).HasColumnType("int");
            builder.Property(x => x.status).HasColumnType("int");
            builder.HasOne(x=>x.Voucher).WithMany(x=>x.VoucherDetail).HasForeignKey(x=>x.IdVoucher);
            builder.HasOne(x=>x.NguoiDung).WithMany(x=>x.voucherDetails).HasForeignKey(x=>x.IdNguoiDung);
        }
    }
}

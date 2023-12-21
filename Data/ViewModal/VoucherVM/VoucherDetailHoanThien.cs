using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.VoucherVM
{
    public class VoucherDetailHoanThien
    {
        public Guid IDVoucherDetail { get; set; }
        public Guid IDVoucher { get; set; }
        public Guid IDNguoiDuong { get; set; }
        public string MaVoucher { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal GiaTriVoucher { get; set; }
        public decimal Min { get ; set; }
        public decimal Max { get ; set; }
        public string DieuKien { get; set; }
        public int Soluong { get; set; }
        public int TrangThai { get; set; }
    }

}

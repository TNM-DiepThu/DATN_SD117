using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.VoucherVM
{
    public class VoucherVM
    {
        public Guid Id { get; set; }
        public string MaVoucher { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal GiaTriVoucher { get; set; }
        public int SoLuong { get; set; }
        public string MoTa { get; set; }
        public string DieuKienGiamGia { get; set; }
        public int TrangThai { get; set; }
    }
}

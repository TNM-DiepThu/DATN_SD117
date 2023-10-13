using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.VoucherVM
{
    public class VoucherDetailVM
    {
        public Guid Id { get; set; }
        public Guid Id_NguoiDung { get; set; }
        public Guid Id_Voucher { get; set; }
        public int SoLuong { get; set; }
        public int TrangThai { get; set; }
    }
}

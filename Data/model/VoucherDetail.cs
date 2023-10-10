using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class VoucherDetail
    {
        public Guid Id { get; set; }
        public int Soluong { get; set; }
        public int status { get; set; }
        public Guid IdVoucher { get; set; }
        public Voucher Voucher { get; set; }
        public Guid IdNguoiDung { get; set; }
        public NguoiDung NguoiDung { get; set;}
        public virtual ICollection<HoaDon> hoaDons { get; set; }
    }
}

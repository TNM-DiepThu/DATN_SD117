using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class NguoiDung : IdentityUser<Guid>
    {
        public string SDT { get; set; }
        public string MatKhau { get; set; }
        public string QuanHuyen { get; set; }
        public string ThanhPho { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public int status { get; set; }
        public virtual ICollection<BinhLuan> BinhLuans { get; set;}
        public virtual ICollection<VoucherDetail> voucherDetails { get; set;}
        public virtual ICollection<LichSuHoaDon> lichSuHoaDons { get; set;}
        public virtual ICollection<HoaDon> hoaDons { get; set;}
        public virtual ICollection<GioHang> gioHangs { get; set;}
    }
}

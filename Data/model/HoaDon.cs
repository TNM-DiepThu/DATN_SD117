using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class HoaDon
    {
        public Guid Id { get; set; }
        public string MaHD { get; set; }
        public DateTime NgayTao { get; set; }
        public int? SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public decimal TienVanChuyen { get; set; }
        public DateTime? NgayGiao { get; set; }
        public DateTime? NgayNhan { get; set; }
        public string? NguoiNhan { get; set; }
        public string? SDT { get; set; }
        public string? QuanHuyen { get; set;}
        public string? Tinh { get; set; }
        public string? DiaChi { get; set;}
        public DateTime? NgayThanhToan { get; set; }
        public string? GhiChu { get; set; }
        public int status { get; set; }
        public Guid IdNguoiDunh { get; set; }
        public NguoiDung? NguoiDung { get; set; }
        public Guid? IdVoucherDetail { get; set; }
        public VoucherDetail? voucherDetail { get; set; }
        public Guid? IDHTTT { get; set; }



        public HinhThucThanhToan HinhThucThanhToan { get; set; }    
        public virtual ICollection<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public virtual ICollection<LichSuHoaDon> LichSuHoaDons { get; set; }
    } 
}

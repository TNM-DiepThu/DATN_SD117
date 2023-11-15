using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class ComboChiTiet
    {
        public Guid Id { get; set; }
        public Guid IdCombo { get; set; }
        public Guid IdSPCT { get; set; }
        public string TenCombo { get; set; }
        public int SoLuongSanPham { get; set; }
        public int SoLuongCombo { get; set; }
        public decimal GiaGoc { get; set; }
        public decimal TienGiamGia { get; set; }
        public decimal GiaBan { get; set; }
        public int TrangThai { get; set; }
       
        public SanPhamChiTiet SanPhamChiTiet { get; set; }
        public Combo combos { get; set; }
        public virtual ICollection<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; }
    }
}

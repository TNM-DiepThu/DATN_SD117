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
        public int SoLuongSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public Guid IdCombo { get; set; }
        public Combo combos { get; set; }
        public Guid IdSPCT { get; set; }
        public SanPhamChiTiet SanPhamChiTiet { get; set; }
        public virtual ICollection<HoaDonChiTiet> hoaDonChiTiets { get; set; }
        public virtual ICollection<GioHangChiTiet> GioHangChiTiets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class HoaDonChiTiet
    {
        public Guid Id { get; set; }
        public int SoLuong { get; set; }
        public int status { get; set; }
        public decimal Gia { get; set; }
        public Guid IDHD { get; set; }
        public HoaDon HoaDon { get; set; }
        public Guid IdCombo { get; set; }
        public ComboChiTiet comboChiTiet { get; set; }
        public Guid IdSPCT { get; set; }
        public SanPhamChiTiet SanPhamChiTiet { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.HoaDon
{
    public class HoaDonCTViewModel
    {
        public Guid IDhdCt { get ; set; }
        public Guid IDHd { get ; set; }
        public Guid? IDSpct { get ; set; }
        public string TenSP { get ; set; }
        public int SoLuong { get; set; }
        public decimal GiaGoc { get; set; } 
        public decimal TienGiamGia { get; set; }
        public decimal GiaBan { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.GioHangChiTietViewModel
{
    public class GioHangChiTietViewModel
    {
        public Guid ID { get; set; }
        public Guid IDGH { get; set; }
        public Guid? IDSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string Mausac { get; set; }
        public string Size { get; set; }
        public decimal GiaSanPham { get; set; }
        public int Soluong { get; set; }
    }
}

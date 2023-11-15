using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.SanPhamChiTietVM
{
    public class ComBoChiTietViewModel
    {
        public Guid Id { get; set; }
        public string TenComBo { get; set; }
        public string TenSp { get; set; }
        public int SoluongCombo { get; set; }
        public decimal GiaGoc { get; set; }    
        public decimal TienGiamGia { get; set; }
        public decimal ThanhTienComBo { get; set; }  
        public decimal SoluongSanpham { get; set; }
    }
}

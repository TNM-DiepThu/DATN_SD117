using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class GioHang
    {
        public Guid Id { get; set; }
        public string GhiChu { get; set; }
        public Guid IdNguoiDung { get; set; }   
        public NguoiDung nguoiDung { get;set; }
        public virtual ICollection<GioHangChiTiet> gioHangChiTiets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class LichSuHoaDon
    {
        public Guid Id { get; set; }
        public DateTime NgayTao { get; set; }
        public string NguoiTao { get; set; }
        public string GhiChu { get; set; }
        public Guid IdNguoiDung { get; set; }
        public NguoiDung nguoiDung { get; set; }
        public Guid IdHoaDon { get; set; }
        public HoaDon HoaDon { get; set; }  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.model
{
    public class HinhThucThanhToan
    {
        public Guid Id { get; set; }
        public string TenHinhThucThanhToan { get; set; }
        public string MoTa { get; set; }
        public int status { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}

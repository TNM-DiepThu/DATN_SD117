using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.Usermodalview
{
    public class NguoiDungVM
    {
        public Guid id { get; set; }
        public string Email { get; set; }
        public string TenNguoiDung { get; set; }
        public string SDT { get; set; }
        public string MatKhau { get; set; }
        public string QuanHuyen { get; set; }
        public string ThanhPho { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public int status { get; set; }
    }
}

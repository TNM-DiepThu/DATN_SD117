using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModal.Usermodalview
{
    public class NguoiDungEditVM
    {

        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string username { get; set; }
        public string TenNguoiDung { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public string MatKhau { get; set; }
        public string QuanHuyen { get; set; }
        public string ThanhPho { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public int GioiTinh { get; set; }
        public int status { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

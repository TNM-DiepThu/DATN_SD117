using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.ViewModal.SanPhamVM
{
    public class SanPhamChiTietVM
    {
        public Guid Id { get; set; }
        public string MaSp { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public string? MoTa { get; set; }

        public int status { get; set; }

        public Guid IDSP { get; set; }
        public Guid IdAnh { get; set; }

        public Guid IdMauSac { get; set; }
        public Guid IdSize { get; set; }
        public Guid IdChatLieu { get; set; }
        public Guid IdDanhMuc { get; set; }
    }
}

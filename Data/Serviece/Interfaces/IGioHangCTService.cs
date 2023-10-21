using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IGioHangCTService
    {
        public bool Add(GioHangChiTiet p);
        public List<GioHangChiTiet> GetAllGioHangTheoNguoiDungDangNhap(Guid idnguoidung);
        public bool Del(Guid id);
        public bool EditSoluong(Guid idghct, int soluong);
        public bool Edit(Guid idghct, GioHangChiTiet p);
    }
}

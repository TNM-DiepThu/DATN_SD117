using AppData.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.Interfaces
{
    public interface IHoaDonService
    {
        public List<HoaDon> GetAll();
        public List<HoaDon> GetAllByIDNguoiDung(Guid IDnguoidung);
        public string AddItem(HoaDon item);
        public bool RemoveItem(HoaDon item);
        public bool EditItem(HoaDon item);
        public HoaDon GetByID(Guid id);
        public bool UpdateDaXacNhan(Guid id);
        public bool UpdateChoLayHang (Guid id );
        public bool UpdateDaLayHang (Guid id );
        public bool UpdateDaThanhToan (Guid id );
        public bool UpdateHuy(Guid id ); 
        public bool UpdateDaNhanHang(Guid id );
        public bool UpdateHDCho (Guid id );
    }
}

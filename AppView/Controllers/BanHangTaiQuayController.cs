using AppData.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class BanHangTaiQuayController : Controller
    {
        // GET: BanHangTaiQuayController
        public BanHangTaiQuayController() { }
        public ActionResult BanHangTaiQuay()
        {
            return View();
        }

        public ActionResult TaoHoaDon(HoaDon hoaDon)
        {
            string url = $"https://localhost:7214/api/HoaDon/TaoHoaDonCho?ngaygiao={hoaDon.NgayGiao}&ngaynhan={hoaDon.NgayGiao}&nguoinhan={hoaDon.NgayGiao}&sdt={hoaDon.NgayGiao}&quanhuyen={hoaDon.NgayGiao}&tinh={hoaDon.NgayGiao}&diachi={hoaDon.NgayGiao}&ngaythanhtoan={hoaDon.NgayGiao}&ghichu={hoaDon.NgayGiao}&idnguoidung={hoaDon.NgayGiao}&idhttt={hoaDon.NgayGiao}";
            return View();
        }
        
    }
}

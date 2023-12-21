using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.ViewModal.HoaDon;
using AppData.ViewModal.VoucherVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace AppView.Controllers
{
    public class VoucherDetailController : Controller
    {
        private readonly ILogger<VoucherDetailController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        private readonly VoucherDetailServices _voucher;
        private readonly IWebHostEnvironment _hosting;

        public VoucherDetailController(ILogger<VoucherDetailController> logger, IWebHostEnvironment hosting)
        {
            _context = new MyDbContext();
            _logger = logger;
            _hosting = hosting;
            _voucher = new VoucherDetailServices();
        }

        public Guid IDNguoiDung()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;

            var userlogin = HttpContext.User;
            var email = userlogin.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(c => c.Email == email);
            return user.Id;
        }
        [HttpGet]
        public ActionResult GetAllVoucherDetail()
        {
            string url = "https://localhost:7214/api/VoucherDetail/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<VoucherDetail> lstVoucher = JsonConvert.DeserializeObject<List<VoucherDetail>>(data);
            return View(lstVoucher);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateVoucherDetail(VoucherDetail voucherDetail)
        {
            string url = $"https://localhost:7214/api/VoucherDetail/Create";
            var obj = JsonConvert.SerializeObject(voucherDetail);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            return View("CreateVoucherDetail");
        }

        [HttpGet]
        [HttpPut]
        public async Task<ActionResult> EditVoucherDetail(VoucherDetail voucherDetail)
        {
            string url = $"https://localhost:7214/api/VoucherDetail/Edit/{voucherDetail.Id}";
            var obj = JsonConvert.SerializeObject(voucherDetail);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);
            return View("GetAllVoucherDetail");
        }

        [HttpGet]
        [HttpDelete]
        public async Task<ActionResult> DeleteVoucherDetail(VoucherDetail voucherDetail)
        {
            string url = $"https://localhost:7214/api/VoucherDetail/Delete/{voucherDetail.Id}";
            var obj = JsonConvert.SerializeObject(voucherDetail);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);

            return RedirectToAction("GellAllCVoucherDetail");
        }
        [HttpGet]
        public async Task<ActionResult> GetListVoucherViewModel()
        {
            string url = $"https://localhost:7214/api/VoucherDetail/GetlistVoucherViewModel";
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> GetListVoucherViewModelByIdNguoiDung()
        {
            var idnguoidung = IDNguoiDung();
            string url = $"https://localhost:7214/api/VoucherDetail/GetlistVoucherViewModelByIdNguoiDung?idnguoidung={idnguoidung}";
            var respone = await _client.GetAsync(url);
            var data = await respone.Content.ReadAsStringAsync();
            List<VoucherDetailHoanThien> listVoucher = JsonConvert.DeserializeObject<List<VoucherDetailHoanThien>>(data);
            return View(listVoucher);
        }
        [HttpGet]
        public ActionResult GetVoucherViewModelById(Guid Idvoucher)
        {
            string url = $"https://localhost:7214/api/VoucherDetail/GetlistVoucherViewModelByID?IdVoucherDetail={Idvoucher}";
            var respone =  _client.GetAsync(url).Result;
            var data =  respone.Content.ReadAsStringAsync().Result;
            VoucherDetailHoanThien VoucherDeatail = JsonConvert.DeserializeObject<VoucherDetailHoanThien>(data);
            return View(VoucherDeatail);
        }
        [HttpGet]
        public ActionResult GetVoucherViewModelByName(string Mavoucher)
        {
            string url = $"https://localhost:7214/api/VoucherDetail/GetListVoucherViewModelByName?MaVoucher={Mavoucher}";
            var respone = _client.GetAsync(url).Result;
            var data = respone.Content.ReadAsStringAsync().Result;
            VoucherDetailHoanThien VoucherDeatail = JsonConvert.DeserializeObject<VoucherDetailHoanThien>(data);
            return Json(VoucherDeatail);
        }
    }
}

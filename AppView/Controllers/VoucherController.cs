using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.ViewModal.VoucherVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class VoucherController : Controller
    {
        private readonly ILogger<VoucherController> _logger;
        HttpClient _client = new HttpClient();
        private readonly MyDbContext _context;
        private readonly VoucherServices _voucher;
        private readonly IWebHostEnvironment _hosting;

        public VoucherController(ILogger<VoucherController> logger, IWebHostEnvironment hosting)
        {
            _context = new MyDbContext();
            _logger = logger;
            _hosting = hosting;
            _voucher = new VoucherServices();
        }

        // GET: VoucherController
        [HttpGet]
        public ActionResult GetAllVoucher()
        {
            string url = "https://localhost:7214/api/Voucher/GetAll";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            List<Voucher> lstVoucher = JsonConvert.DeserializeObject<List<Voucher>>(data);
            return View(lstVoucher);
        }

        // GET: VoucherController/Details/5
        [HttpGet]
        public async Task<ActionResult> GetVoucherById(Guid id)
        {
            string url = $"https://localhost:7214/api/Voucher/GetVooucherById?id={id}";
            var respon = _client.GetAsync(url).Result;
            var data = respon.Content.ReadAsStringAsync().Result;
            VoucherVM voucherVM = JsonConvert.DeserializeObject<VoucherVM>(data);
            return View(data);
        }

        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> CreateVoucher(Voucher voucher)
        {
            string url = $"https://localhost:7214/api/Voucher/Create";
            var obj = JsonConvert.SerializeObject(voucher);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PostAsync(url, content);
            return View("CreateVoucher");
        }

        [HttpGet]
        [HttpPut]
        public async Task<ActionResult> EditVoucher(Voucher voucher)
        {
            string url = $"https://localhost:7214/api/Voucher/Edit/{voucher.Id}";
            var obj = JsonConvert.SerializeObject(voucher);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);
            return View("GetAllVoucher");
        }

        [HttpGet]
        [HttpDelete]
        public async Task<ActionResult> DeleteVoucher(Voucher voucher)
        {
            string url = $"https://localhost:7214/api/Voucher/Delete/{voucher.Id}";
            var obj = JsonConvert.SerializeObject(voucher);
            StringContent content = new StringContent(obj, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _client.PutAsync(url, content);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var Error = await httpResponseMessage.Content.ReadAsStringAsync();
                if (Error == "true")
                {

                    TempData["SuccessFull"] = "Ok ";
                    return RedirectToAction("GellAllSanPhamCT", "QuanTri");
                }
                else
                {
                    TempData["ErrorMessage"] = "Xóa thất bại.";
                    return RedirectToAction("GellAllSanPhamCT", "QuanTri");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Xóa thất bại.";
                return RedirectToAction("GellAllVoucher", "QuanTri");
            }
            //return RedirectToAction("GellAllCVoucher");
        }
    }
}

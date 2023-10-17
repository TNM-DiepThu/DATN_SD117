using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}

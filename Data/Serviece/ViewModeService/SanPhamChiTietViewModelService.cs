using Bill.Serviece.Implements;
using Bill.Serviece.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Serviece.ViewModeService
{
    public class SanPhamChiTietViewModelService
    {
        private readonly ISanPhamChiTietServiece _spctservice;
        private readonly ISanPhamServiece _spservice;
        private readonly IMauSacServiece _smacserviece;
        private readonly IThuongHieuServiece _thservice;
        private readonly ISizeServiece _sservice;
        private readonly IDanhMucServiece _dmservice;
        private readonly IAnhServiece _aservice;
        public SanPhamChiTietViewModelService()
        {
            _aservice = new AnhServiece();
            _spctservice = new SanPhamChiTietServiece();
            _spservice = new SanPhamServiece();
            _smacserviece = new MauSacServiece();
            _thservice = new ThuongHieuServiece();
            _dmservice = new DanhMucServiece();
        }
        public List<SanPhamChiTietViewModelService> SanPhamChiTiet()
        {
            // viet linQ
            return new List<SanPhamChiTietViewModelService>();

        }

    }
}

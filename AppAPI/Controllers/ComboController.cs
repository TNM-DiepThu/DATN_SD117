using AppData.data;
using AppData.model;
using AppData.Serviece.Implements;
using AppData.Serviece.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboService comBoSer;
        private MyDbContext _dbContext = new MyDbContext();

        public ComboController()
        {
            comBoSer = new ComboService();
        }
        [HttpGet("[action]")]

        public IEnumerable<Combo> GetAllAsync()
        {
            return comBoSer.GetAll();
        }
        [HttpPost("[action]")]
        public bool CreateCombo(string Ten, string mota, decimal phantramgiam)
        {
            Combo combo = new Combo()
            {
                Id = Guid.NewGuid(),
                TenCombo = Ten,
                MoTaCombo = mota,
                PhanTramGiam = phantramgiam,
                status = 1,
            };
            if (comBoSer.GetAll().Any(c => c.TenCombo == Ten))
            {
                return false;
            }

            return comBoSer.Add(combo);
        }
        [HttpPost("[action]")]

        public bool DeleteCombo(Guid Id)
        {
            var cb = comBoSer.GetAll().FirstOrDefault(c => c.Id == Id);
            cb.status = 0;
            return comBoSer.Edit(cb.Id, cb);
        }

        [HttpPut("[action]")]

        public bool UpdateCombo(Guid id, string Ten, string mota, decimal phantram)
        {
            var cb = _dbContext.combos.FirstOrDefault(p => p.Id == id);
            cb.TenCombo = Ten;
            cb.MoTaCombo = mota;
            cb.PhanTramGiam = phantram;
            return comBoSer.Edit(cb.Id, cb);
        }
        [HttpGet("[action]")]
        public Combo GetbyIDComBo(Guid id)
        {
            return _dbContext.combos.FirstOrDefault(p => p.Id == id);
        }
    }
}

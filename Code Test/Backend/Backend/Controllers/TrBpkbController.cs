using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrBpkbController : ControllerBase
    {
        private readonly MCF_DavinContext _context;

        public TrBpkbController(MCF_DavinContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetTrBpkb()
        {
            return Ok(await _context.TrBpkbs.FirstOrDefaultAsync());
        }

        [HttpPost]
        public async Task<ActionResult> AddTrBpkb(TrBpkb trBpkb)
        {
            _context.TrBpkbs.Add(trBpkb);
            await _context.SaveChangesAsync();

            return Ok(await _context.TrBpkbs.FirstOrDefaultAsync());
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTrBpkb(TrBpkb trBpkb)
        {
            var dbTrBpkb = await _context.TrBpkbs.FindAsync(trBpkb.AgreementNumber);
            if (dbTrBpkb == null)
                _context.TrBpkbs.Add(trBpkb);
            else
            {
                dbTrBpkb.AgreementNumber = trBpkb.AgreementNumber;
                dbTrBpkb.BpkbNo = trBpkb.BpkbNo;
                dbTrBpkb.BranchId = trBpkb.BranchId;
                dbTrBpkb.BpkbDate = trBpkb.BpkbDate;
                dbTrBpkb.FakturNo = trBpkb.FakturNo;
                dbTrBpkb.FakturDate = trBpkb.FakturDate;
                dbTrBpkb.LocationId = trBpkb.LocationId;
                dbTrBpkb.PoliceNo = trBpkb.PoliceNo;
                dbTrBpkb.BpkbDateIn = trBpkb.BpkbDateIn;
                dbTrBpkb.CreatedBy = trBpkb.CreatedBy;
                dbTrBpkb.CreatedOn = trBpkb.CreatedOn;
                dbTrBpkb.LastUpdatedBy = trBpkb.LastUpdatedBy;
                dbTrBpkb.LastUpdatedOn = trBpkb.LastUpdatedOn;
            }

            await _context.SaveChangesAsync();

            return Ok(await _context.TrBpkbs.FirstOrDefaultAsync());
        }
    }
}

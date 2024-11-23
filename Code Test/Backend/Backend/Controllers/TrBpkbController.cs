using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

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
        [Route("GetListBpkb")]
        public async Task<ActionResult> GetListBpkb()
        {
            return Ok(await _context.TrBpkbs.ToListAsync());
        }

        [HttpGet]
        [Route("GetBpkbByAgreementNumber")]
        public async Task<ActionResult> GetBpkbByAgreementNumber(string agreementNumber)
        {
            return Ok(await _context.TrBpkbs.Where(x=>x.AgreementNumber == agreementNumber).FirstOrDefaultAsync());
        }

        [HttpPost]
        [Route("AddBpkb")]
        public async Task<ActionResult> AddBpkb(TrBpkb trBpkb)
        {
            trBpkb.CreatedOn=DateTime.Now;
            trBpkb.CreatedBy = "sysadmin";

            _context.TrBpkbs.Add(trBpkb);
            await _context.SaveChangesAsync();

            return Ok(await _context.TrBpkbs.FirstOrDefaultAsync());
        }

        [HttpPost]
        [Route("UpdateBpkb")]
        public async Task<ActionResult> UpdateBpkb(TrBpkb trBpkb)
        {
            var dbTrBpkb = await _context.TrBpkbs.FindAsync(trBpkb.AgreementNumber);
            if (dbTrBpkb == null)
                return NotFound(new { Message = "Bpkb record not found." });

            dbTrBpkb.AgreementNumber = trBpkb.AgreementNumber;
            dbTrBpkb.BpkbNo = trBpkb.BpkbNo;
            dbTrBpkb.BranchId = trBpkb.BranchId;
            dbTrBpkb.BpkbDate = trBpkb.BpkbDate;
            dbTrBpkb.FakturNo = trBpkb.FakturNo;
            dbTrBpkb.FakturDate = trBpkb.FakturDate;
            dbTrBpkb.LocationId = trBpkb.LocationId;
            dbTrBpkb.PoliceNo = trBpkb.PoliceNo;
            dbTrBpkb.BpkbDateIn = trBpkb.BpkbDateIn;
            dbTrBpkb.LastUpdatedOn = DateTime.Now;
            dbTrBpkb.LastUpdatedBy = "sysadmin";

            await _context.SaveChangesAsync();
            return Ok(await _context.TrBpkbs.FirstOrDefaultAsync());
        }

        [HttpDelete]
        [Route("DeleteBpkb")]
        public async Task<ActionResult> DeleteBpkb(string agreementNumber)
        {
            var trBpkb = await _context.TrBpkbs.FindAsync(agreementNumber);
            if (trBpkb == null)
                return NotFound(new { Message = "Bpkb record not found." });

            _context.TrBpkbs.Remove(trBpkb);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Bpkb record deleted successfully." });
        }
    }
}

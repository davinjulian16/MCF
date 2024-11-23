using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly MCF_DavinContext _context;

        public LocationController(MCF_DavinContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult> GetListLocations()
        {
            return Ok(await _context.MsStorageLocations.ToListAsync());
        }
    }
}

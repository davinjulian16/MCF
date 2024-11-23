using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly MCF_DavinContext _context;

        public UserController(MCF_DavinContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> login(MsUser msUser)
        {
            if (_context.MsUsers.Where(x => x.UserName == msUser.UserName && x.Password == msUser.Password).Count() > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { Message = "User not found or invalid password" });
            }
        }
    }
}

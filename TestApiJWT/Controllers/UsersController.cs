using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApiJWT.Models;

namespace TestApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetUser(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet, Route("admins")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAdmins()
        {
            List<ApplicationUser> admins = new List<ApplicationUser>();
            var roleAdminId = _context.Roles.FirstOrDefault(r => r.Name == "Admin").Id;
            foreach(var item in _context.UserRoles.ToList())
            {
                if(item.RoleId == roleAdminId)
                {
                    admins.Add(await _context.Users.FindAsync(item.UserId));
                }
            }
            return admins;
        }
    }
}

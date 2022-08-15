using ApplicationDevelopment.Data;
using ApplicationDevelopment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApplicationDevelopment.Controllers
{
    public class StoreOwnerController : Controller
    {
        public ApplicationDbContext _context;
        public UserManager<ApplicationUser> _userManager; 

        public StoreOwnerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        //public async Task<IActionResult> IndexAsync()
        //{
        //    var usersInRole = await _userManager.GetUsersInRoleAsync("Customer");

        //}       

    }
}

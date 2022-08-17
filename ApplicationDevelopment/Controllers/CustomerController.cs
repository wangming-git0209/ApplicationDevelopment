using ApplicationDevelopment.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationDevelopment.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

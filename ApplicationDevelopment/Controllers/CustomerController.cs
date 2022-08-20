using ApplicationDevelopment.Data;
using ApplicationDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

    

        [HttpGet]
        public IActionResult Home()
        {
            List<Book> listBook = _context.Books.Include(b => b.Category).ToList();

            return View(listBook);
        }

        [HttpGet]
        public IActionResult AddItemToOrder(int id)
        {
            if(id != 0)
            {
                return RedirectToAction("Index", id);
            }

            return View();
        }
    }
}

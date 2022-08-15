using ApplicationDevelopment.Data;
using ApplicationDevelopment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationDevelopment.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            List<Book> listBook = _context.Books.Include(b => b.Category).ToList();

            //var listCategories = _context.Categories.Include(c => c.Books);

            //foreach (var c in listCategories)
            //{
            //    // 2 cách đều được
            //    //listBook = _context.Books.Where(b => b.CategoryId != c.Id || b.CategoryId == c.Id).ToList();
            //    listBook = _context.Books.Include(b => b.Category).ToList();
            //}

            return View(listBook);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var listCategories = _context.Categories.ToList();
            ViewData["listCategories"] = listCategories;
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {

            var newBook = new Book
            {
                NameBook = book.NameBook,
                InformationBook = book.InformationBook,
                Price = book.Price,
                QuantityBook = book.QuantityBook,
                CategoryId = book.CategoryId,
                CreatedAt = System.DateTime.Now

            };

            _context.Books.Add(newBook);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Book bookToDelete = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id); 
            if (bookToDelete == null)
            {
                return BadRequest();
            }

            _context.Remove(bookToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Book book = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            return View(book); 
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var listCategories = _context.Categories.ToList();
            ViewData["listCategories"] = listCategories;
           
            Book book = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Update(Book book)
        {
            var bookUpdate = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == book.Id);

            bookUpdate.NameBook = book.NameBook;
            bookUpdate.InformationBook = book.InformationBook;
            bookUpdate.Price = book.Price;
            bookUpdate.QuantityBook = book.QuantityBook;
            bookUpdate.CategoryId = book.CategoryId;
            bookUpdate.CreatedAt = System.DateTime.Now;

            _context.Books.Update(bookUpdate);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }





    }
}

using ApplicationDevelopment.Data;
using ApplicationDevelopment.Models;
using ApplicationDevelopment.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApplicationDevelopment.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IFormFile ProfileImage { get; set; }

        public BookController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        private string UploadedFile(BookViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
        public IActionResult Create(BookViewModel model)
        {

            string uniqueFileName = UploadedFile(model);

            Book newBook = new Book
            {
                NameBook = model.book.NameBook,
                InformationBook = model.book.InformationBook,
                Price = model.book.Price,
                QuantityBook = model.book.QuantityBook,
                CategoryId = model.book.CategoryId,
                CreatedAt = System.DateTime.Now,
                ProfilePicture = uniqueFileName,

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

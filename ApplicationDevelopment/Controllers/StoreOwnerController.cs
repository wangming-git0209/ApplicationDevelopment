﻿using ApplicationDevelopment.Data;
using ApplicationDevelopment.Models;
using ApplicationDevelopment.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> IndexAsync(List<ApplicationUser> listCustomer)
        {
            if (listCustomer.Count != 0)
            {
                return View(listCustomer);
            }
            var usersInRole = await _userManager.GetUsersInRoleAsync(Role.CUSTOMER);
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string searchString)
        {

            if (searchString == null)
            {
                return NotFound();
            }
            return await SearchCustomerAsync(searchString);


        }

        [NonAction]
        public async Task<IActionResult> SearchCustomerAsync(string searchString)
        {
            List<ApplicationUser> listCustomer = new List<ApplicationUser>();
            if (searchString == null)
            {
                return NotFound();
            }
            var customer = await _userManager.FindByEmailAsync(searchString);
            listCustomer.Add(customer);

            return View("Index", listCustomer);
        }


        [HttpGet]
        public IActionResult SendCategoryRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendCategoryRequest(Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            Category newCategory = new Category();

            newCategory.NameCategory = category.NameCategory;
            newCategory.Status = Enums.CategoryStatus.InProgess;

            _context.Add(newCategory);
            _context.SaveChanges();

            return View(newCategory);
        }

        [HttpGet]
        public IActionResult ListCustomerOrder()
        {
            return View();
        }
    }
}

﻿using ApplicationDevelopment.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ApplicationDevelopment.ViewModels
{
    public class BookViewModel
    {
        public Book book { set; get; }


        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }
    }
}

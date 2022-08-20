using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace ApplicationDevelopment.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name of Book can not be null")]
        [StringLength(255)]
        public string NameBook { get; set; }
        [Required(ErrorMessage = "Quantity can not be null")]
        public int QuantityBook { get; set; }
        [Required(ErrorMessage = "Price can not be null")]
        public int Price { get; set; }
        public string InformationBook { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

using BookRentalProject.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Security;
using System.Web.Mvc;

namespace BookRentalProject.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(8)]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Availibility { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }


        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Date Added")]
        [DisplayFormat(DataFormatString ="{0:MM dd yyyy}")]
        public DateTime? DateAdded { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Publication Date:")]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime PublicationDate { get; set; }


        [Required]
        public string ProductDimensions { get; set; }

        [Required]
        public int Pages { get; set; }

    }
}
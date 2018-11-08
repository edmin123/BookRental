using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalProject.Models
{
    public class Genre
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Genre Name:")]
        public string Name { get; set; }
    }
}
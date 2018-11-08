using BookRentalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRentalProject.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
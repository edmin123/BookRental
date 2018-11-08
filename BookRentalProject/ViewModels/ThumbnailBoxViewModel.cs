using BookRentalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRentalProject.ViewModels
{
    public class ThumbnailBoxViewModel
    {
        public IEnumerable<ThumbnailModel> Thumbnails { get; set; }
    }
}
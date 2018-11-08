using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookRentalProject.Models;
using BookRentalProject.Extensions;
using BookRentalProject.ViewModels;


namespace BookRentalProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = ApplicationDbContext.Create();
        }
        public ActionResult Index(string search=null)
        {
            var thumbnails = new List<ThumbnailModel>().getAllThumbnails(db,search);
            var count = thumbnails.Count() / 4;
            var thumbnailBox= new List<ThumbnailBoxViewModel>();
            for(int i = 0; i <= count; i++)
            {
                thumbnailBox.Add(new ThumbnailBoxViewModel
                {
                    Thumbnails = thumbnails.Skip(i * 4).Take(4)
                });
            }

            

            return View(thumbnailBox);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
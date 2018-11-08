using BookRentalProject.Models;
using BookRentalProject.Utility;
using BookRentalProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookRentalProject.Controllers
{
    public class BookDetailController : Controller
    {
        private ApplicationDbContext db;

        public BookDetailController()
        {
            db = ApplicationDbContext.Create();
        }

        // GET: BookDetail
        public ActionResult Index(int id)
        {
            
            var bookSelected = db.Books.FirstOrDefault(b => b.Id == id);
            var userId = User.Identity.GetUserId();

            if (bookSelected == null)
            {
                return HttpNotFound();
            }

            var user = db.Users.SingleOrDefault(u => u.Id.Equals(userId));

            var chargeRate = from u in db.Users
                             join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                             where u.Id.Equals(userId)
                             select new
                             {
                                 m.ChargeRateOneMonth,
                                 m.ChargeRateSixMonth
                             };

            var rentalPrice = 0.0;
            var chargeRateOneMonth = 0.0;
            var chargeRateSixMonth = 0.0;

            if(user!=null && !User.IsInRole(SD.AdminUserRole))
            {
                chargeRateOneMonth = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth)/100;
                chargeRateSixMonth = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;

            }


            var bookRentVM = new BookRentViewModel
            {
                UserId=userId,
                BookId=bookSelected.Id,
                RentalPrice=rentalPrice,
                ImageUrl=bookSelected.ImageUrl,
                Title=bookSelected.Title,
                Author=bookSelected.Author,
                ISBN=bookSelected.ISBN,
                Price=bookSelected.Price,
                Description=bookSelected.Description,
                rentalPriceOneMonth=chargeRateOneMonth,
                rentalPriceSixMonth=chargeRateSixMonth,
                GenreId=bookSelected.GenreId,
                Genre=db.Genres.Where(g=>g.Id==bookSelected.GenreId).FirstOrDefault(),
                DateAdded=bookSelected.DateAdded,
                ProductDimensions=bookSelected.ProductDimensions,
                Availibility=bookSelected.Availibility
            };

            return View(bookRentVM);
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
using BookRentalProject.Models;
using BookRentalProject.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookRentalProject.Controllers.Api
{
    public class BooksApiController : ApiController
    {
        private ApplicationDbContext db;

        public BooksApiController()
        {
            db = ApplicationDbContext.Create();
        }

        //Get Title
        public IHttpActionResult Get(string query = null)
        {
            var bookQuery = db.Books.Where(m => m.ISBN.ToLower().Contains(query.ToLower()));
            return Ok(bookQuery.ToList());
        }

        //Get Price or Avail
        public IHttpActionResult Get(string type, string isbn=null,string email=null,string rentalDuration=null)
        {
            if (type.Equals("price"))
            {
                var bookSelected = db.Books.Where(b => b.ISBN.ToLower().Contains(isbn)).FirstOrDefault();
                var price = 0.0;
                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Email.Equals(email)
                                 select new
                                 {
                                     m.ChargeRateOneMonth,
                                     m.ChargeRateSixMonth
                                 };
                if (rentalDuration.Equals(SD.OneMonthValue))
                {
                    price = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth)/100;
                }
                else
                {
                    price = Convert.ToDouble(bookSelected.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth)/100;

                }

                return Ok(price);
            }
            else
            {
                var bookSelected = db.Books.Where(b => b.ISBN.ToLower().Contains(isbn)).FirstOrDefault();
                return Ok(bookSelected.Availibility);
            }

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

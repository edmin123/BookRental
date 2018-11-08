using BookRentalProject.Models;
using BookRentalProject.Utility;
using BookRentalProject.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

namespace BookRentalProject.Controllers
{
    [Authorize]
    public class BookRentController : Controller
    {
        private ApplicationDbContext db;

        public BookRentController()
        {
            db = ApplicationDbContext.Create();
        }
        // GET: BookRent
        public ActionResult Index(int? pageNumber, string search=null,string option=null)
        {
            string userId = User.Identity.GetUserId();
            var bookRents = from br in db.BookRents
                            join u in db.Users on br.UserId equals u.Id
                            join b in db.Books on br.BookId equals b.Id
                            select new BookRentViewModel
                            {
                                UserId=u.Id,
                                BookId=b.Id,
                                ImageUrl=b.ImageUrl,
                                ISBN=b.ISBN,
                                Pages=b.Pages,
                                Price=b.Price,
                                ProductDimensions=b.ProductDimensions,
                                PublicationDate=b.PublicationDate,
                                RentalDuration=br.RentalDuration,
                                RentalPrice=br.RentalPrice,
                                GenreId=b.GenreId,
                                Genre=db.Genres.Where(g=>g.Id==b.GenreId).FirstOrDefault(),
                                Description=b.Description,
                                DateAdded=b.DateAdded,
                                Availibility=b.Availibility,
                                Author=b.Author,
                                Title=b.Title,
                                Birthday=u.Birthday,
                                FirstName=u.FirstName,
                                LastName=u.LastName,
                                Email=u.Email,
                                ActualEndDate=br.ActualEndDate,
                                StartDate=br.StartDate,
                                AdditionalCharge=br.AdditionalCharge,
                                ScheduledEndDate=br.ScheduledEndDate,
                                Id=br.Id,
                                Status=br.Status.ToString()
                                


                            };

            if(option=="email" && search.Length > 0)
            {
                bookRents = bookRents.Where(u => u.Email.ToLower().Contains(search.ToLower()));

            }

            if (option=="name" && search.Length > 0)
            {
                bookRents = bookRents.Where(u => u.FirstName.ToLower().Contains(search.ToLower()) || u.LastName.ToLower().Contains(search.ToLower()));

            }

            if (option=="status" && search.Length > 0)
            {
                bookRents = bookRents.Where(u => u.Status.ToLower().Contains(search.ToLower()));

            }


            if (!User.IsInRole(SD.AdminUserRole))
            {
                bookRents = bookRents.Where(u => u.UserId.Equals(userId));
                return View(bookRents.ToList().ToPagedList(pageNumber ?? 1, 5));
            }

            return View(bookRents.ToList().ToPagedList(pageNumber?? 1,5));
        }

        public ActionResult Create(string isbn=null,string title = null)
        {
            if(isbn!=null && title != null)
            {
                var model = new BookRentViewModel
                {
                    Title = title,
                    ISBN = isbn

                };
                return View(model);
            }

            return View(new BookRentViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookRentViewModel bookRentVM)
        {
            if (ModelState.IsValid)
            {
                var email = bookRentVM.Email;
                var book = db.Books.Where(b => b.ISBN.Equals(bookRentVM.ISBN)).SingleOrDefault();

                var userDetails = from u in db.Users
                                  where u.Email.Equals(email)
                                  select new
                                  {
                                      u.Id,
                                      u.FirstName,
                                      u.LastName,
                                      u.Email
                                  };

                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Email.Equals(email)
                                 select new
                                 {
                                     m.ChargeRateOneMonth,
                                     m.ChargeRateSixMonth
                                 };
                var rentalPrice = 0.0;

                if (bookRentVM.RentalDuration.Equals(SD.OneMonthValue))
                {
                    rentalPrice = Convert.ToDouble(book.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;
                }

                if (bookRentVM.RentalDuration.Equals(SD.SixMonthValue))
                {
                    rentalPrice = Convert.ToDouble(book.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;
                }

                BookRent bookRentToAddToDb = new BookRent
                {
                    UserId = userDetails.ToList()[0].Id,
                    BookId = book.Id,
                    RentalDuration = bookRentVM.RentalDuration,
                    RentalPrice = rentalPrice,
                    Status = BookRent.StatusEnum.Approved,
                    StartDate = bookRentVM.StartDate,
                    ActualEndDate = bookRentVM.ActualEndDate,
                    AdditionalCharge = bookRentVM.AdditionalCharge,
                    ScheduledEndDate = bookRentVM.ScheduledEndDate
                };

                db.BookRents.Add(bookRentToAddToDb);
                book.Availibility -= 1;
                db.SaveChanges();
                return RedirectToAction("Index");
                
                

            }

            return View(bookRentVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve(BookRentViewModel bookRentVM)
        {
            string userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            if (user!=null)
            {
                
                var book = db.Books.Find(bookRentVM.BookId);

                var rentalPrice = 0.0;

                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Id.Equals(userId)
                                 select new
                                 {
                                     m.ChargeRateOneMonth,
                                     m.ChargeRateSixMonth
                                 };

                if (bookRentVM.RentalDuration.Equals(SD.OneMonthValue))
                {
                    rentalPrice = Convert.ToDouble(book.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;
                }
                else
                {
                    rentalPrice = Convert.ToDouble(book.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;
                }

                var bookRentToAddToDb = new BookRent
                {
                    UserId = userId,
                    BookId = book.Id,
                    ActualEndDate = bookRentVM.ActualEndDate,
                    StartDate = bookRentVM.StartDate,
                    AdditionalCharge = bookRentVM.AdditionalCharge,
                    RentalDuration = bookRentVM.RentalDuration,
                    RentalPrice = rentalPrice,
                    ScheduledEndDate = bookRentVM.ScheduledEndDate,
                    Status = BookRent.StatusEnum.Requested

                };

                book.Availibility -= 1;
                db.BookRents.Add(bookRentToAddToDb);
                db.SaveChanges();
                return RedirectToAction("Index");



            }
            return View(bookRentVM);

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookRent = db.BookRents.Find(id);
            var model = getVMFromBookRent(bookRent);

            return View(model);

        }

        public ActionResult Decline(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookRent = db.BookRents.Find(id);
            var model = getVMFromBookRent(bookRent);

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Decline(BookRentViewModel bookRentVMModel)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(bookRentVMModel.BookId);
                var bookRent = db.BookRents.Find(bookRentVMModel.Id);
                bookRent.Status = BookRent.StatusEnum.Rejected;
                book.Availibility += 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }



        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookRent = db.BookRents.Find(id);
            var model = getVMFromBookRent(bookRent);

            return View("Approve",model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(BookRentViewModel bookRentVMModel)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(bookRentVMModel.BookId);
                var bookRent = db.BookRents.Find(bookRentVMModel.Id);
                bookRent.Status = BookRent.StatusEnum.Approved;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public ActionResult PickUp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookRent = db.BookRents.Find(id);
            var model = getVMFromBookRent(bookRent);

            return View("Approve", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PickUp(BookRentViewModel bookRentVMModel)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(bookRentVMModel.BookId);
                var bookRent = db.BookRents.Find(bookRentVMModel.Id);
                bookRent.Status = BookRent.StatusEnum.Rented;
                bookRent.StartDate = DateTime.Now;
                if (bookRent.RentalDuration.Equals(SD.SixMonthValue))
                {
                    bookRent.ScheduledEndDate = DateTime.Now.AddMonths(Convert.ToInt32(SD.SixMonthValue));
                }
                else
                {
                    bookRent.ScheduledEndDate = DateTime.Now.AddMonths(Convert.ToInt32(SD.OneMonthValue));
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookRent = db.BookRents.Find(id);
            var model = getVMFromBookRent(bookRent);

            return View("Approve", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(BookRentViewModel bookRentVMModel)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(bookRentVMModel.BookId);
                var bookRent = db.BookRents.Find(bookRentVMModel.Id);
                bookRent.Status = BookRent.StatusEnum.Closed;
                bookRent.ActualEndDate = DateTime.Now;
                bookRent.AdditionalCharge = bookRentVMModel.AdditionalCharge;
                book.Availibility += 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bookRent = db.BookRents.Find(id);
            var model = getVMFromBookRent(bookRent);

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var bookRent = db.BookRents.Find(id);
                var book = db.Books.SingleOrDefault(b => b.Id == bookRent.BookId);
                db.BookRents.Remove(bookRent);
                if ((bookRent.Status.ToString().ToLower().Equals(SD.ApprovedToLower)) || (bookRent.Status.ToString().ToLower().Equals(SD.RentedToLower)))
                {
                    book.Availibility += 1;
                }

                db.BookRents.Remove(bookRent);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        private BookRentViewModel getVMFromBookRent(BookRent bookRent)
        {
            var userDetails = from u in db.Users
                              where u.Id.Equals(bookRent.UserId)
                              select new
                              {
                                  u.FirstName,
                                  u.LastName,
                                  u.Email,
                                  u.Birthday,
                                  u.Phone,
                                  u.MembershipTypeId,
                                  
                              };

            var book = db.Books.Find(bookRent.BookId);


            var model = new BookRentViewModel
            {
                Id = bookRent.Id,
                UserId = bookRent.UserId,
                BookId = bookRent.BookId,
                StartDate = bookRent.StartDate,
                ActualEndDate = bookRent.ActualEndDate,
                ScheduledEndDate = bookRent.ScheduledEndDate,
                AdditionalCharge = bookRent.AdditionalCharge,
                RentalPrice=bookRent.RentalPrice,
                RentalDuration=bookRent.RentalDuration,
                Pages=book.Pages,
                Price=book.Price,
                Status = bookRent.Status.ToString(),
                FirstName = userDetails.ToList()[0].FirstName,
                LastName = userDetails.ToList()[0].LastName,
                Email = userDetails.ToList()[0].Email,
                Birthday = userDetails.ToList()[0].Birthday,
                Phone = userDetails.ToList()[0].Phone,
                Author = book.Author,
                Availibility = book.Availibility,
                Description = book.Description,
                GenreId = book.GenreId,
                DateAdded = book.DateAdded,
                ImageUrl=book.ImageUrl,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate,
                Title=book.Title,
                Genre = db.Genres.FirstOrDefault(g => g.Id == book.GenreId)





            };

            return (model);

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
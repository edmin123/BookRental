using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookRentalProject.Models;
using BookRentalProject.Utility;
using BookRentalProject.ViewModels;

namespace BookRentalProject.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Genre);
            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var bookModel = new BookViewModel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };

            return View(bookModel);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var bookModel = new BookViewModel
            {
                Genres = db.Genres.ToList()
            };
            return View(bookModel);
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(BookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Author = bookVM.Book.Author,
                    Availibility = bookVM.Book.Availibility,
                    DateAdded = bookVM.Book.DateAdded,
                    Pages = bookVM.Book.Pages,
                    Description = bookVM.Book.Description,
                    GenreId = bookVM.Book.GenreId,
                    ImageUrl = bookVM.Book.ImageUrl,
                    ISBN = bookVM.Book.ISBN,
                    Price = bookVM.Book.Price,
                    ProductDimensions = bookVM.Book.ProductDimensions,
                    PublicationDate = bookVM.Book.PublicationDate,
                    Title = bookVM.Book.Title,
                    Genre = db.Genres.Where(g => g.Id == bookVM.Book.GenreId).FirstOrDefault()
                    
                };


                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            bookVM.Genres = db.Genres.ToList();
            return View(bookVM);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var bookModel = new BookViewModel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };

            return View(bookModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(BookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                var bookInDb = db.Books.Find(bookVM.Book.Id);

                bookInDb.Author = bookVM.Book.Author;
                bookInDb.Availibility = bookVM.Book.Availibility;
                bookInDb.DateAdded = bookVM.Book.DateAdded;
                bookInDb.Pages = bookVM.Book.Pages;
                bookInDb.Description = bookVM.Book.Description;
                bookInDb.GenreId = bookVM.Book.GenreId;
                bookInDb.ImageUrl = bookVM.Book.ImageUrl;
                bookInDb.ISBN = bookVM.Book.ISBN;
                bookInDb.Price = bookVM.Book.Price;
                bookInDb.ProductDimensions = bookVM.Book.ProductDimensions;
                bookInDb.PublicationDate = bookVM.Book.PublicationDate;
                bookInDb.Title = bookVM.Book.Title;
                bookInDb.Genre = db.Genres.Where(g => g.Id == bookVM.Book.GenreId).FirstOrDefault();

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            bookVM.Genres = db.Genres.ToList();
            return View(bookVM);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var bookModel = new BookViewModel
            {
                Book = book,
                Genres = db.Genres.ToList()
            };

            return View(bookModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

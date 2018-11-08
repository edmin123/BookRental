using BookRentalProject.Models;
using BookRentalProject.Utility;
using BookRentalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookRentalProject.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]
    public class UserController : Controller
    {
        private ApplicationDbContext db;

        public UserController()
        {
            db = ApplicationDbContext.Create();
        }

        // GET: User
        public ActionResult Index()
        {
            var users = from u in db.Users
                        join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                        select new UserViewModel
                        {
                            FirstName=u.FirstName,
                            LastName=u.LastName,
                            Birthday=u.Birthday,
                            Id=u.Id,
                            MembershipTypeId=u.MembershipTypeId,
                            Email=u.Email,
                            Disable=u.Disable,
                            MembershipTypes=(ICollection<MembershipType>) db.MembershipTypes.Where(model=>model.Id==m.Id),
                            Phone=u.Phone

                        };
            var usersList = users.ToList();

            return View(usersList);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var usermodel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Id = user.Id,
                MembershipTypeId = user.MembershipTypeId,
                Email = user.Email,
                Disable = user.Disable,
                MembershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone
            };

            return View(usermodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userVMModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.Users.Find(userVMModel.Id);
                user.FirstName = userVMModel.FirstName;
                user.LastName = userVMModel.LastName;
                user.Phone = userVMModel.Phone;
                user.Email = userVMModel.Email;
                user.Birthday = userVMModel.Birthday;
                user.MembershipTypeId = userVMModel.MembershipTypeId;
                user.Disable = userVMModel.Disable;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            userVMModel.MembershipTypes = db.MembershipTypes.ToList();
            return View(userVMModel);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var usermodel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Id = user.Id,
                MembershipTypeId = user.MembershipTypeId,
                Email = user.Email,
                Disable = user.Disable,
                MembershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone
            };

            return View(usermodel);

        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var usermodel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Id = user.Id,
                MembershipTypeId = user.MembershipTypeId,
                Email = user.Email,
                Disable = user.Disable,
                MembershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone
            };

            return View(usermodel);

        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            user.Disable = true;
            db.SaveChanges();
            return RedirectToAction("Index");

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
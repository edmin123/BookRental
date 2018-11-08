using BookRentalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookRentalProject.Controllers.Api
{
    public class UsersApiController : ApiController
    {
        private ApplicationDbContext db;

        public UsersApiController()
        {
            db = ApplicationDbContext.Create();
        }

        //email & name & burthday
        public IHttpActionResult Get(string type, string query = null)
        {
            if (type.Equals("email") && query!=null)
            {
                var userQuery = db.Users.Where(u => u.Email.ToLower().Contains(query.ToLower()));
                return Ok(userQuery.ToList());
            }
            if (type.Equals("name") && query!=null)
            {
                var userQuery = from u in db.Users
                                where u.Email.Equals(query)
                                select new
                                {
                                    u.FirstName,
                                    u.LastName,
                                    u.Birthday
                                };
                return Ok(userQuery.ToList()[0].FirstName + " " + userQuery.ToList()[0].LastName + ";" + userQuery.ToList()[0].Birthday);
            }

            return Ok();
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

using BookRentalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookRentalProject.Extensions
{
    public static class ThumbnailExtension
    {
        public static IEnumerable<ThumbnailModel> getAllThumbnails(this List<ThumbnailModel> thumbnails,ApplicationDbContext db,string search=null)
        {
            if (db == null)
            {
                db = ApplicationDbContext.Create();
            }

            thumbnails = (from b in db.Books
                          select new ThumbnailModel
                          {
                              BookId = b.Id,
                              Title = b.Title,
                              Description = b.Description,
                              ImageUrl = b.ImageUrl,
                              Link = "/BookDetail/Index/" + b.Id
                          }).ToList();

            if (search != null)
            {
                return thumbnails.Where(m => m.Title.ToLower().Contains(search.ToLower())).OrderBy(t => t.Title);
            }

            return thumbnails.OrderBy(m => m.Title);
        }
    }
}
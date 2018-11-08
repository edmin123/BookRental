using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BookRentalProject.Models
{
    public class IndividualButtonModel
    {
        public string ButtonType { get; set; }

        public string ActionName { get; set; }

        public string Glyphc { get; set; }

        public int? GenreId { get; set; }

        public int? BookId { get; set; }

        public int? MembershipTypeId { get; set; }

        public string UserId { get; set; }

        public int? BookRentId { get; set; }

        public string ActionParametars
        {
            get
            {
                var param = new StringBuilder(@"/");

                if (BookId!=null && BookId > 0)
                {
                    param.Append(String.Format("{0}", BookId));
                }

                if (GenreId != null && GenreId > 0)
                {
                    param.Append(String.Format("{0}", GenreId));
                }

                if (MembershipTypeId != null && MembershipTypeId > 0)
                {
                    param.Append(String.Format("{0}", MembershipTypeId));
                }

                if (UserId != null && UserId.Trim().Length > 0)
                {
                    param.Append(String.Format("{0}", UserId));
                }

                if (BookRentId != null && BookRentId > 0)
                {
                    param.Append(String.Format("{0}", BookRentId));
                }

                return param.ToString();
            }
        }

    }
}
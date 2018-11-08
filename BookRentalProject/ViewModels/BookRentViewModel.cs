using BookRentalProject.Models;
using BookRentalProject.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalProject.ViewModels
{
    public class BookRentViewModel
    {
        //BookRent properties
        public int Id { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }


        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Actual End Date")]
        public DateTime? ActualEndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Scheduled End Date")]
        public DateTime? ScheduledEndDate { get; set; }


        [DataType(DataType.Currency)]
        [Display(Name = "Rental Price")]
        public double RentalPrice { get; set; }

        [Display(Name = "Rental Duration")]
        public string RentalDuration { get; set; }


        [DataType(DataType.Currency)]
        [Display(Name = "Additional Charge")]
        public double? AdditionalCharge { get; set; }

        public double rentalPriceOneMonth { get; set; }

        public double rentalPriceSixMonth { get; set; }

        public string Status { get; set; }



        //User properties
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        //Book Details

        [MinLength(8)]
        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Availibility { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }


        [DataType(DataType.Currency)]
        public double Price { get; set; }


        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime? DateAdded { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Publication Date:")]
        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        public DateTime PublicationDate { get; set; }


        public string ProductDimensions { get; set; }

        public int Pages { get; set; }

        public string ActionName
        {
            get
            {
                if (Status.ToLower().Equals(SD.RequestedToLower))
                {
                    return "Approve";
                }

                if (Status.ToLower().Equals(SD.ApprovedToLower))
                {
                    return "PickUp";
                }
                if (Status.ToLower().Equals(SD.RentedToLower))
                {
                    return "Return";
                }
                return null;
            }
        }

    }
}
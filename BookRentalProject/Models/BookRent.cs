using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalProject.Models
{
    public class BookRent
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int BookId { get; set; }


        [DisplayFormat(DataFormatString ="{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name ="Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Actual End Date")]
        public DateTime? ActualEndDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Scheduled End Date")]
        public DateTime? ScheduledEndDate { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        [Display(Name ="Rental Price")]
        public double RentalPrice { get; set; }

        [Required]
        [Display(Name = "Rental Duration")]
        public string  RentalDuration { get; set; }


        [DataType(DataType.Currency)]
        [Display(Name = "Additional Charge")]
        public double? AdditionalCharge { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        public enum StatusEnum
        {
            Requested,
            Approved,
            Rejected,
            Rented,
            Closed
        }

    }
}
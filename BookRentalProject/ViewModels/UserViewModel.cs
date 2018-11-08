using BookRentalProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalProject.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name ="First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:MM dd yyyy}")]
        public DateTime Birthday { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        public ICollection<MembershipType> MembershipTypes { get; set; }


        public bool Disable { get; set; }
    }
}
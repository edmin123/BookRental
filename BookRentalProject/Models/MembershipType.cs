using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalProject.Models
{
    public class MembershipType
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Sign Up Fee")]
        public byte SignUpFee { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Charge Rate One Month")]
        public byte ChargeRateOneMonth { get; set; }

        [Required]
        [Display(Name = "Charge Rate Six Month")]
        public byte ChargeRateSixMonth { get; set; }
    }
}
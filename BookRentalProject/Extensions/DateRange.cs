using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookRentalProject.Extensions
{
    public class DateRange : RangeAttribute
    {
        public DateRange(string minValue) : base(typeof(DateTime), minValue, DateTime.Now.ToShortDateString())
        {

        }
    }
}
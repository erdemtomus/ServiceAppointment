using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TofasRandevu.ViewModels
{
    public class ScheduleDate
    {
        [Display(Name = "Randevu Tarihi")]
        public string Date { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TofasRandevu.Models;

namespace TofasRandevu.ViewModels
{
    public class AppointmentLocation
    {

        [Display(Name = "Randevu Tarihi")]
        [Required]
        public string AppointmentDate { get; set; }

        [Display(Name = "Şehir")]
        [Required]
        public string CityCode { get; set; }
        public string CityName { get; set; }

        //[Display(Name = "İlçe")]
        //[Required]
        //public string TownCode { get; set; }

        [Display(Name = "Servis")]
        [Required]
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
    }
}
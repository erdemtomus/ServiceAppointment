using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TofasRandevu.ViewModels
{
    public class OperationInformation
    {

        [Display(Name = "Marka")]
        [Required]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        [Required]
        public string Model { get; set; }

        [Display(Name = "Araç KM bilgisi")]
        [Required]
        public int? VehicleKM { get; set; }

        [Display(Name = "Yapılacak İşlem")]
        [Required]
        public string Definition1 { get; set; }

        [Display(Name = "Yapılacak İşlem Açıklaması")]
        [Required]
        [MaxLength(200, ErrorMessage = "En fazla 200 karakter açıklama girebilirsiniz...")]
        public string Definition2 { get; set; }

        [Display(Name = "Ulaşım Aracınız Var mı?")]
        [Required]
        public bool NeedTransportation { get; set; }

        [Display(Name = "Arıza Işığı Yanıyor mu?")]
        [Required]
        public bool FailureWarning { get; set; }
    }
}
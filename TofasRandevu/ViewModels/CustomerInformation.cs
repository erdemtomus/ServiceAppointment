using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TofasRandevu.ViewModels
{
    public class CustomerInformation
    {
        [Display(Name = "Adınız Soyadınız")]
        [Required(ErrorMessage="Ad Soyad alanı gereklidir...")]
        public string Name { set; get; }

        [Display(Name = "Telefon Numaranız")]
        [Required(ErrorMessage = "Telefon numarası alanı gereklidir...")]        
        //[DataType(DataType.PhoneNumber,ErrorMessage= "Geçerli telefon numarası giriniz...")]
        //[RegularExpression(@"\([1-9][0-9]{2}\) [0-9]{3}-[0-9]{4}", ErrorMessage = "Telefon numaranızın başında 0 olmadan 10 haneli olarak giriniz...")]
        [RegularExpression(@"\(([1-9][0-9]{2})\) ([0-9]{3})-([0-9]{4})", ErrorMessage = "Telefon numaranızın başında 0 olmadan 10 haneli olarak giriniz...")]
        public string PhoneNumber { set; get; }

        [Display(Name = "E-Posta Adresiniz")]
        [Required(ErrorMessage = "E-Posta alanı gereklidir...")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Geçerli E-Posta adresi giriniz...")]
        public string Email { get; set; }

        [Display(Name = "Araç Plakası")]
        [Required(ErrorMessage = "Araç plakası alanı gereklidir...")]
        [RegularExpression(@"(0[1-9]|[1-7][0-9]|8[01])(([a-zA-Z])(\d{4,5})|([a-zA-Z]{2})(\d{3,4})|([a-zA-Z]{3})(\d{2}))", ErrorMessage = "Geçerli plaka giriniz...")]
        public string Plate { get; set; }

        [Display]
        [Required]
        public bool AcceptDataPrivacy { get; set; }
                
    }
}
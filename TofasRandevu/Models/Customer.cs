using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TofasRandevu.Models
{
    [DataContract]
    public class CustomerRequest
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "plate")]
        public string Plate { get; set; }
    }

    [DataContract]
    public class Customer
    {
        [DataMember(Name = "seqNo")]
        public int? SeqNo { get; set; }

        [DataMember(Name = "sasiNo")]
        public string SasiNo { get; set; }

        [DataMember(Name = "motorNo")]
        public string MotorNo { get; set; }

        [DataMember(Name = "uniteNo")]
        public string UniteNo { get; set; }

        [DataMember(Name = "marka")]
        public string Marka { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "seri")]
        public string Seri { get; set; }

        [DataMember(Name = "versiyon")]
        public string Versiyon { get; set; }

        [DataMember(Name = "renk")]
        public string Renk { get; set; }

        [DataMember(Name = "modelYil")]
        public int? ModelYil { get; set; }

        [DataMember(Name = "plaka")]
        public string Plate { get; set; }

        [DataMember(Name = "rezervationId")]
        public string RezervationId { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
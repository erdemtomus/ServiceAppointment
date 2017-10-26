using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TofasRandevu.Models
{
    [DataContract]
    public class Representative
    {
        [DataMember(Name="persId")]
        public string PersonalId { get; set; }

        [DataMember(Name = "bayiKodu")]
        public string DealerCode { get; set; }

        [DataMember(Name = "adi")]
        public string Name { get; set; }

        [DataMember(Name = "soyadi")]
        public string Surname { get; set; }

        [DataMember(Name = "cinsiyet")]
        public string Gender { get; set; }

        [DataMember(Name = "wasPersKodu")]
        public string PersonalCode { get; set; }
        
    }
}
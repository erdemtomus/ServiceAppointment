using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TofasRandevu.Models
{
    [DataContract]
    public class Service
    {
        [DataMember(Name = "cmpCode")]
        public string CompanyCode { get; set; }

        [DataMember(Name = "cmpName")]
        public string CompanyName { get; set; }

        [DataMember(Name = "cmpCusvenCode")]
        public string CustomerVendorCode { get; set; }

        [DataMember(Name = "adress1")]
        public string Address1 { get; set; }

        [DataMember(Name = "adress2")]
        public string Address2 { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "tel")]
        public string Tel { get; set; }

        [DataMember(Name = "fax")]
        public string Fax { get; set; }
    }
}
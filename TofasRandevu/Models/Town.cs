using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TofasRandevu.Models
{
    [DataContract]
    public class Town
    {
        [DataMember(Name="cityCode")]
        public string CityCode { get; set; }

        [DataMember(Name = "explanation")]
        public string Explanation { get; set; }
    }
}
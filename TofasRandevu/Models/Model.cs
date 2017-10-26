using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TofasRandevu.Models
{
    [DataContract]
    public class Model
    {

        [DataMember(Name = "modelKodu")]
        public string ModelCode { get; set; }

        [DataMember(Name = "modelAciklama")]
        public string ModelDefinition { get; set; }
    }
}


using System;
using System.Runtime.Serialization;
namespace TofasRandevu.Models
{
    [DataContract]
    public class Appointment
    {
        [DataMember(Name = "cmpCode")]
        public string CompanyCode { get; set; }


        [DataMember(Name = "appSeq")]
        public string Sequence { get; set; }


        [DataMember(Name = "cusvenName")]
        public string CustomerName { get; set; }


        [DataMember(Name = "creDate")]
        public string CreatedDate { get; set; }


        [DataMember(Name = "creUser")]
        public string CreatedUser { get; set; }


        [DataMember(Name = "appDate")]
        public string AppointmentDate { get; set; }


        [DataMember(Name = "definition")]
        public string Definition { get; set; }

        [DataMember(Name = "definition1")]
        public string Definition1 { get; set; }


        [DataMember(Name = "tel3")]
        public string Telephone { get; set; }


        [DataMember(Name = "status")]
        public string Status { get; set; }


        [DataMember(Name = "email")]
        public string Email { get; set; }


        [DataMember(Name = "plaka")]
        public string Plate { get; set; }


        [DataMember(Name = "marka")]
        public string Brand { get; set; }


        [DataMember(Name = "model")]
        public string Model { get; set; }


        [DataMember(Name = "versiyon")]
        public string Version { get; set; }


        [DataMember(Name = "seri")]
        public string Serial { get; set; }


        [DataMember(Name = "aracSeqNo")]
        public int? VehicleSequenceNumber { get; set; }


        [DataMember(Name = "personelKodu")]
        public string PersonalCode { get; set; }


        [DataMember(Name = "aracKm")]
        public int? VehicleKm { get; set; }


        [DataMember(Name = "ulasimIhtiyaci")]
        public string NeedTransportation { get; set; }


        [DataMember(Name = "arizaIsigi")]
        public string FailureWarning { get; set; }

        [IgnoreDataMember]
        public string CityName { get; set; }

        [IgnoreDataMember]
        public string ServiceName { get; set; }

        [IgnoreDataMember]
        public string PersonalName { get; set; }

    }
}
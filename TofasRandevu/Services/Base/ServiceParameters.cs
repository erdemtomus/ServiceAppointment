using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TofasRandevu.Services.Base
{
    public class ServiceParameters
    {
        public static string Username
        {
            get
            {
                return ConfigurationManager.AppSettings["username"]; ;
            }
        }
        public static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["password"]; ;
            }
        }

        public static string URL
        {
            get
            {
                return ConfigurationManager.AppSettings["serviceUrl"]; ;
            }
        }
        public static int? Port
        {
            get
            {
                string portStr = ConfigurationManager.AppSettings["port"];
                if (string.IsNullOrEmpty(portStr))
                {
                    return null;
                }
                else
                {

                    return int.Parse(portStr);
                }
            }
        }

        public static readonly string EndPoint = "/bos-api/appointment/";
        public static readonly string ApplyMethodUrl = "/apply";
        public static readonly string CitiesMethodUrl = "/cities";
        public static readonly string TownsMethodUrl = "/towns";
        public static readonly string ServicesMethodUrl = "/services";
        public static readonly string RepresentativeMethodUrl = "/representatives";
        public static readonly string PeriodsMethodUrl = "/periods";
        public static readonly string SchedulerMethodUrl = "/scheduler";
        public static readonly string CreateAppointmentMethodUrl = "/createAppointment";
        public static readonly string ReserveMethodUrl = "/rezervation";
        public static readonly string DeleteReserveMethodUrl = "/deleterezervation";
        public static readonly string ModelsMethodUrl = "/models";
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using TofasRandevu.Models;
using TofasRandevu.Services.Base;

namespace TofasRandevu.Services
{
    public class CustomerService : ICustomerService
    {
        private RestService restService;
        public CustomerService()
        {
            restService = new RestService(ServiceParameters.URL, ServiceParameters.Port, ServiceParameters.EndPoint, ServiceParameters.Username, ServiceParameters.Password);
        }
        public Response<Customer> Apply(CustomerRequest customerRequestObject)
        {
            var result = restService.SendJsonRequest<Customer>(HttpMethod.Post, ServiceParameters.ApplyMethodUrl, customerRequestObject);
            return result;
        }

        public Response<IEnumerable<Representative>> GetRepresentativeList(string companyCode, string rezervationId)
        {
            return restService.SendRequest<IEnumerable<Representative>>(HttpMethod.Get, ServiceParameters.RepresentativeMethodUrl + "/" + companyCode + "/" + rezervationId);
        }

        public Response<IEnumerable<DateTime>> GetPeriods(DateTime date, string rezervationId)
        {
            var parameter = date.Date.ToString("dd-MM-yyyy");
            var result = restService.SendRequest<IEnumerable<string>>(HttpMethod.Get, ServiceParameters.PeriodsMethodUrl + "/" + parameter + "/" + rezervationId);
            var periods = result.Data.Select(x =>
            {
                return DateTime.ParseExact(x, "yyyyMMddHHmm", CultureInfo.CurrentCulture);
            }).ToList();
            return new Response<IEnumerable<DateTime>>
            {
                Code = result.Code,
                Data = periods
            };
        }

        public Response<IEnumerable<Appointment>> GetSchedules(string companyCode, DateTime date, string rezervationId)
        {
            var day = date.Date.ToString("dd-MM-yyyy");
            var data = restService.SendRequest<IEnumerable<Appointment>>(HttpMethod.Get, ServiceParameters.SchedulerMethodUrl + "/" + companyCode + "/" + day + "/" + rezervationId);
            return data;
        }

        public Response<bool> CreateAppointment(Appointment appointment, string rezervationId)
        {
            return restService.SendJsonRequest<bool>(HttpMethod.Post, ServiceParameters.CreateAppointmentMethodUrl + "/" + rezervationId, appointment);
        }

        public Response<bool> Reserve(string companyCode, string personalCode, string date, string rezervationId)
        {
            return restService.SendRequest<bool>(HttpMethod.Get, ServiceParameters.ReserveMethodUrl + "/" + companyCode + "/" + personalCode + "/" + date + "/" + rezervationId);
        }

        public Response<bool> DeleteReserve(string companyCode, string personalCode, string date, string rezervationId)
        {
            return restService.SendRequest<bool>(HttpMethod.Get, ServiceParameters.DeleteReserveMethodUrl + "/" + companyCode + "/" + personalCode + "/" + date + "/" + rezervationId);
        }
        public Response<IEnumerable<Model>> GetModels()
        {
            return restService.SendRequest<IEnumerable<Model>>(HttpMethod.Get, ServiceParameters.ModelsMethodUrl);
        }
    }
}
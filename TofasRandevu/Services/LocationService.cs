using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using TofasRandevu.Models;
using TofasRandevu.Services.Base;

namespace TofasRandevu.Services
{
    public class LocationService : ILocationService
    {

        private RestService restService;
        public LocationService()
        {
            restService = new RestService(ServiceParameters.URL, ServiceParameters.Port, ServiceParameters.EndPoint, ServiceParameters.Username, ServiceParameters.Password);
        }

        public Response<IEnumerable<City>> GetCityList()
        {
            return restService.SendRequest<IEnumerable<City>>(HttpMethod.Get, ServiceParameters.CitiesMethodUrl);
        }

        public Response<IEnumerable<Town>> GetTownList(string cityCode)
        {
            return restService.SendRequest<IEnumerable<Town>>(HttpMethod.Get, ServiceParameters.TownsMethodUrl + "/" + cityCode);
        }

        public Response<IEnumerable<Service>> GetServiceList(string townCode, string rezervationId)
        {
            var response = restService.SendRequest<IEnumerable<Service>>(HttpMethod.Get, ServiceParameters.ServicesMethodUrl + "/" + townCode + "/" + rezervationId);
            response.Data = response.Data.OrderBy(x => x.CompanyName);
            return response;
        }

    }
}
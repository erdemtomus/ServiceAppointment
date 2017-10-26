using System.Collections.Generic;
using TofasRandevu.Models;

namespace TofasRandevu.Services.Base
{
    public interface ILocationService
    {
        Response<IEnumerable<City>> GetCityList();
        Response<IEnumerable<Town>> GetTownList(string cityCode);
        Response<IEnumerable<Service>> GetServiceList(string townCode, string rezervationId);
    }
}
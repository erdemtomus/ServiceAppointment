using System;
using System.Collections.Generic;
using TofasRandevu.Models;
namespace TofasRandevu.Services.Base
{
    interface ICustomerService
    {
        Response<Customer> Apply(CustomerRequest customerRequestObject);
        Response<IEnumerable<Representative>> GetRepresentativeList(string companyCode, string rezervationId);
        Response<IEnumerable<DateTime>> GetPeriods(DateTime date, string rezervationId);
        Response<IEnumerable<Appointment>> GetSchedules(string companyCode, DateTime date, string rezervationId);
        Response<bool> CreateAppointment(Appointment appointment, string rezervationId);
        Response<bool> Reserve(string companyCode, string personalCode, string date, string rezervationId);
        Response<bool> DeleteReserve(string companyCode, string personalCode, string date, string rezervationId);
        Response<IEnumerable<Model>> GetModels();
    }
}

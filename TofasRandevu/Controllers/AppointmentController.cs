using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TofasRandevu.Services;
using TofasRandevu.Services.Base;

namespace TofasRandevu.Controllers
{
    [RoutePrefix("Appointment")]
    public class AppointmentController : Controller
    {
        private ICustomerService customerService;
        public AppointmentController()
        {
            this.customerService = new CustomerService();
        }

        [HttpGet]
        [Route("Reserve/{companyCode}/{personalCode}/{date}/{rezervationId}")]
        public JsonResult Reserve(string companyCode, string personalCode, string date, string rezervationId)
        {
            return Json(customerService.Reserve(companyCode, personalCode, date, rezervationId).Data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("DeleteReserve/{companyCode}/{personalCode}/{date}/{rezervationId}")]
        public JsonResult DeleteReserve(string companyCode, string personalCode, string date, string rezervationId)
        {
            return Json(customerService.DeleteReserve(companyCode, personalCode, date, rezervationId).Data, JsonRequestBehavior.AllowGet);
        }
    }
}
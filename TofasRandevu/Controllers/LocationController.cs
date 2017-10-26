using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TofasRandevu.Models;
using TofasRandevu.Services;
using TofasRandevu.Services.Base;

namespace TofasRandevu.Controllers
{
    [RoutePrefix("Location")]
    public class LocationController : Controller
    {
        private ILocationService locationService;
        public LocationController()
        {
            this.locationService = new LocationService();
        }

        [HttpGet]
        [Route("GetTownList/{cityCode}")]
        public JsonResult GetTownList(string cityCode)
        {
            return Json(locationService.GetTownList(cityCode).Data,JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [Route("GetServiceList/{townCode}/{rezervationId}")]
        public JsonResult GetServiceList(string townCode, string rezervationId)
        {
            return Json(locationService.GetServiceList(townCode,rezervationId).Data, JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TofasRandevu.ViewModels;

namespace TofasRandevu.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(ErrorMessage errorMessage)
        {
            return View(errorMessage);
        }

    }
}
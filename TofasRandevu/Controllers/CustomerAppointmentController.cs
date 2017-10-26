using BotDetect.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TofasRandevu.Models;
using TofasRandevu.Services;
using TofasRandevu.Services.Base;
using TofasRandevu.ViewModels;

namespace TofasRandevu.Controllers
{

    public class CustomerAppointmentController : Controller
    {
        private ILocationService locationService;
        private ICustomerService customerService;

        public CustomerAppointmentController()
        {
            this.locationService = new LocationService();
            this.customerService = new CustomerService();
        }


        public ActionResult CustomerInformation()
        {
            ViewBag.PlateCheckStatus = "Default";
            bool PassedCaptcha = Session["PassedCaptcha"] == null ? false : (bool)Session["PassedCaptcha"];
            bool AcceptDataPrivacy = Session["PassedCaptcha"] == null ? false : (bool)Session["PassedCaptcha"];
            ViewBag.PassedCaptcha = PassedCaptcha;
            ViewBag.AcceptDataPrivacy = AcceptDataPrivacy;
            return View();
        }


        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Geçersiz kod girdiniz!")]
        public ActionResult CustomerInformation(CustomerInformation customerInformation, bool captchaValid, string SubmitType)
        {
            Regex regex = new Regex(@"\(([1-9][0-9]{2})\) ([0-9]{3})-([0-9]{4})");
            Match match = regex.Match(customerInformation.PhoneNumber);
            customerInformation.PhoneNumber = match.Groups[1].Value + match.Groups[2].Value + match.Groups[3].Value;
            customerInformation.Plate = customerInformation.Plate.ToUpper();
            customerInformation.Name = customerInformation.Name.ToUpper(CultureInfo.GetCultureInfo("tr-TR"));
            bool PassedCaptcha = Session["PassedCaptcha"] == null ? false : (bool)Session["PassedCaptcha"];
            bool AcceptDataPrivacy = Session["PassedCaptcha"] == null ? customerInformation.AcceptDataPrivacy : (bool)Session["PassedCaptcha"];
            Session["PassedCaptcha"] = AcceptDataPrivacy;
            ViewBag.AcceptDataPrivacy = AcceptDataPrivacy;
            if (!captchaValid && PassedCaptcha == false)
            {
                Session["PassedCaptcha"] = false;
                ViewBag.PlateCheckStatus = "Default";
                ViewBag.PassedCaptcha = false;
                return View(customerInformation);
            }
            else
            {
                Session["PassedCaptcha"] = true;
                MvcCaptcha.ResetCaptcha("ExampleCaptcha");
                CustomerRequest customerRequest = new CustomerRequest
                {
                    Name = customerInformation.Name,
                    Phone = customerInformation.PhoneNumber,
                    Email = customerInformation.Email,
                    Plate = customerInformation.Plate
                };
                var result = customerService.Apply(customerRequest);
                if (result.Code == HttpStatusCode.Conflict)
                {
                    return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)result.Code, Message = "Database hatası" });
                }
                else if (result.Code != HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)result.Code, Message = "Web servis hatası" });
                }
                if (result.Data.SasiNo == null)
                {
                    if (SubmitType == "ContinueWithCheck" || SubmitType == "Retry")
                    {
                        ViewBag.PlateCheckStatus = "NotFound";
                        ViewBag.PassedCaptcha = true;
                        return View(customerInformation);
                    }
                }
                Customer customer = result.Data;
                customer.Email = customerRequest.Email;
                customer.Phone = customerRequest.Phone;
                customer.Name = customerRequest.Name;
                customer.Plate = customerRequest.Plate;
                Session["rezervationId"] = customer.RezervationId;
                Session["appointment"] = new Appointment
                {
                    CustomerName = customer.Name,
                    Telephone = customer.Phone,
                    Email = customer.Email,
                    Status = "NEW",
                    Plate = customer.Plate,
                    Brand = customer.Marka,
                    Model = customer.Model,
                    Version = customer.Versiyon,
                    Serial = customer.Seri,
                    VehicleSequenceNumber = customer.SeqNo
                };
                return RedirectToAction("Location");
            }
        }

        public ActionResult Location()
        {
            var result = locationService.GetCityList();
            if (result.Code == HttpStatusCode.Conflict)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)result.Code, Message = "Database hatası" });
            }
            else if (result.Code != HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)result.Code, Message = "Web servis hatası" });
            }
            IEnumerable<City> provinceList = result.Data;
            ViewBag.Cities = new SelectList(provinceList.Select(x => new { Value = x.CityCode, Text = x.Explanation }), "Value", "Text");
            var list = provinceList.Where(x => x.CityCode.Equals(01)).Select(x => x.CityCode).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Location(AppointmentLocation appointmentLocation)
        {
            Appointment appointment = (Appointment)Session["appointment"];
            if (appointment == null)
            {
                return RedirectToAction("CustomerInformation");
            }
            appointment.CompanyCode = appointmentLocation.ServiceCode;
            appointment.CityName = appointmentLocation.CityName;
            appointment.ServiceName = appointmentLocation.ServiceName;
            return RedirectToAction("Schedule", new ScheduleDate { Date = appointmentLocation.AppointmentDate });
        }

        [HttpGet]
        public ActionResult Schedule(ScheduleDate scheduleDate)
        {
            Appointment appointment = (Appointment)Session["appointment"];
            if (appointment == null)
            {
                return RedirectToAction("CustomerInformation");
            }
            string rezervationId = (string)Session["rezervationId"];
            DateTime time;
            try
            {
                time = DateTime.ParseExact(scheduleDate.Date, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                time = DateTime.Now;
            }

            var representativesResult = customerService.GetRepresentativeList(appointment.CompanyCode, rezervationId);
            if (representativesResult.Code == HttpStatusCode.Conflict)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)representativesResult.Code, Message = "Database hatası" });
            }
            else if (representativesResult.Code != HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)representativesResult.Code, Message = "Web servis hatası" });
            }

            var periodsResult = customerService.GetPeriods(time, rezervationId);
            if (periodsResult.Code == HttpStatusCode.Conflict)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)periodsResult.Code, Message = "Database hatası" });
            }
            else if (periodsResult.Code != HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)periodsResult.Code, Message = "Web servis hatası" });
            }

            var schedulesResult = customerService.GetSchedules(appointment.CompanyCode, time, rezervationId);
            if (schedulesResult.Code == HttpStatusCode.Conflict)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)schedulesResult.Code, Message = "Database hatası" });
            }
            else if (schedulesResult.Code != HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)schedulesResult.Code, Message = "Web servis hatası" });
            }

            ViewBag.Schedules = schedulesResult.Data;
            ViewBag.Representatives = representativesResult.Data;
            ViewBag.Periods = periodsResult.Data;
            ViewBag.CompanyCode = appointment.CompanyCode;
            return View(scheduleDate);
        }

        [HttpPost]
        public ActionResult Schedule(string personalCode, string personalName, string date)
        {
            Appointment appointment = (Appointment)Session["appointment"];
            if (appointment == null)
            {
                return RedirectToAction("CustomerInformation");
            }
            appointment.PersonalCode = personalCode;
            appointment.PersonalName = personalName;
            appointment.AppointmentDate = date;
            return RedirectToAction("Operation");
        }


        public ActionResult Operation()
        {
            var resultModels = customerService.GetModels();
            if (resultModels.Code == HttpStatusCode.Conflict)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)resultModels.Code, Message = "Database hatası" });
            }
            else if (resultModels.Code != HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)resultModels.Code, Message = "Web servis hatası" });
            }
            ViewBag.BrandList = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            ViewBag.ModelList = new SelectList(resultModels.Data.Select(x => new { Value = x.ModelDefinition, Text = x.ModelDefinition }), "Value", "Text");
            ViewBag.DefinitionList = 
                new SelectList(
                    new[] { 
                        new { Value = "Periyodik bakım", Text = "Periyodik bakım" },
                        new { Value = "Mini hasar", Text = "Mini hasar" },
                        new { Value = "Hasar", Text = "Hasar" },
                        new { Value = "Arıza", Text = "Arıza" }}, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult Operation(OperationInformation operationInformation)
        {
            Appointment appointment = (Appointment)Session["appointment"];
            if (appointment == null)
            {
                return RedirectToAction("CustomerInformation");
            }
            string rezervationId = (string)Session["rezervationId"];
            appointment.Definition = operationInformation.Definition2;
            appointment.Definition1 = operationInformation.Model + " / " + operationInformation.Definition1;
            appointment.FailureWarning = operationInformation.FailureWarning ? "Y" : "N";
            appointment.NeedTransportation = operationInformation.NeedTransportation ? "Y" : "N";
            appointment.VehicleKm = operationInformation.VehicleKM;
            var response = customerService.CreateAppointment(appointment, rezervationId);
            if (response.Code == HttpStatusCode.Conflict)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)response.Code, Message = "Database hatası" });
            }
            else if (response.Code != HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Error", new ErrorMessage { Code = (int)response.Code, Message = "Web servis hatası" });
            }
            return RedirectToAction("Result", response);
        }

        public ActionResult Result(Response<bool> response)
        {
            Appointment appointment = (Appointment)Session["appointment"];
            if (appointment == null)
            {
                return RedirectToAction("CustomerInformation");
            }
            ResultViewModel vm = new ResultViewModel
            {
                Status = response.Data,
                ServiceName = appointment.ServiceName,
                CityName = appointment.CityName,
                RepresentativeName = appointment.PersonalName,
                Date = DateTime.ParseExact(appointment.AppointmentDate,"yyyyMMddHHmm",CultureInfo.CurrentCulture).ToString("dd.MM.yyyy HH:mm")
            };
            Session.Clear();
            return View(vm);
        }


    }

}
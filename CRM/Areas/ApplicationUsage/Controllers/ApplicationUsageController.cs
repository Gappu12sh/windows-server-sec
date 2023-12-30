using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Customer.Model;
using Customer.BusinessLogic.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Web.Http.Results;

namespace CRM.Areas.ApplicationUsage.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class ApplicationUsageController : BaseController
    {
        private readonly IApplicationUsage _applicationUsage;

        public ApplicationUsageController(IApplicationUsage applicationUsage)
        {
            _applicationUsage = applicationUsage;
        }

        // GET: ApplicationUsage/ApplicationUsage
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
            return View();
        }

        [HttpPost]
        public string AddApplicationUsage(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<ApplicationUsageModel>(data.ToString());
                var result = _applicationUsage.CreateApplicationUsage(getData);
                responseData = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of ApplicationUsage Controller.");
            }
            return responseData;
        }
        [HttpPut]
        public string UpdateApplicationUsage(object data)
        {
            string response = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<ApplicationUsageModel>(data.ToString());
                var result = _applicationUsage.UpdateApplicationUsage(getData);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of ApplicationUsage Controller.");
            }
            return response;
        }
        [HttpGet]
        public string GetDetails()
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                var result = _applicationUsage.GetApplicationUsage();
                responseData = JsonConvert.SerializeObject(new List<ApplicationUsageModel>(result.ResponseBody));
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
            return responseData;
        }
        [HttpGet]
        public string GetDetailsById(int id)
        {
            var response = new HttpResponseMessage();
            string responseData = String.Empty;

            try
            {
                var result = _applicationUsage.GetApplicationUsageById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
            return responseData;
        }
        [HttpDelete]
        public string DeleteDetails(int id)
        {
            string response = String.Empty;

            try
            {
               var result= _applicationUsage.DeleteApplicationUsage(id);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
            return response;
        }
    }
}
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

namespace CRM.Areas.Caste.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class DepartmentController : BaseController
    {
        // GET: Caste/Caste
        public ActionResult Index()
        {
            //ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
           
            return View();
        }
       
        [HttpPost]
        public string AddEditDeleteCaste(string data)
        {
            string response = String.Empty;
            try
            {
                //ViewBag.UserInfo = UserInfo;
                ViewBag.UserPermissionDetails = UserPermissionDetails;
                ViewBag.PageSize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
                string url = webAPIPort + "Caste/AddEditDeleteCaste";
                logger.Info(data);
                response = Commonfunction.PostHttpWebRequestAPICall(url, data);
                logger.Info(response);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Caste Controller.");
            }
            return response;
        }
        [HttpPost]
        public string GetDetails(string data)
        {
            string response = String.Empty;

            try
            {
                //ViewBag.UserInfo = UserInfo;
                ViewBag.UserPermissionDetails = UserPermissionDetails;
                ViewBag.PageSize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
                string url = webAPIPort + "Caste/GetDetails";
                logger.Info(data);
                response = Commonfunction.PostHttpWebRequestAPICall(url, data);
                logger.Info(response);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of CasteDetails Controller.");
            }
            return response;
        }
        [HttpPost]
        public string DeleteDetails(string data)
        {
            string response = String.Empty;

            try
            {
                //ViewBag.UserInfo = UserInfo;
                ViewBag.UserPermissionDetails = UserPermissionDetails;
                ViewBag.PageSize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
                string url = webAPIPort + "Caste/DeleteDetails";
                logger.Info(data);
                response = Commonfunction.PostHttpWebRequestAPICall(url, data);
                logger.Info(response);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of CasteDetails Controller.");
            }
            return response;
        }
    }
}
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

namespace CRM.Areas.ChangePassword.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class ChangePasswordController : BaseController
    {
        // GET: ChangePassword/ChangePassword
        public ActionResult Index()
        {
            //ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            //if (Session["MasterData"] == null)
            //{
            //    GetMasterData();
            //}
            //else
            //{
            //    MasterData obj = (MasterData)Session["MasterData"];
            //    ProcessMasterData(obj);
            //}
            return View();
        }
        [HttpPost]
        public string AddEditDeleteChangePassword(string data)
        {
            string response = String.Empty;
            try
            {
                //ViewBag.UserInfo = UserInfo;
                ViewBag.UserPermissionDetails = UserPermissionDetails;
                ViewBag.PageSize = Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
                string url = webAPIPort + "ChangePassword/AddEditDeleteChangePassword";
                logger.Info(data);
                response = Commonfunction.PostHttpWebRequestAPICall(url, data);
                logger.Info(response);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of ChangePassword Controller.");
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
                string url = webAPIPort + "ChangePassword/GetDetails";
                logger.Info(data);
                response = Commonfunction.PostHttpWebRequestAPICall(url, data);
                logger.Info(response);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of ChangePasswordDetails Controller.");
            }
            return response;
        }
       
    }
}
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
using Customer.Data.Application;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CRM.Areas.UserModulePermission.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class UserModulePermissionController : BaseController
    {
        private readonly IUserModulePermission _UserModulePermission;
        private readonly IUser _user;

        public UserModulePermissionController(IUserModulePermission UserModulePermission, IUser user)
        {
            _UserModulePermission = UserModulePermission;
            _user = user;
        }

        // GET: UserModulePermission/UserModulePermission
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            GetModules();
            GetUsers();
            return View();
        }
        public void GetModules()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _UserModulePermission.GetModules();
                responseData = JsonConvert.SerializeObject(new List<ModulesModel>(result.ResponseBody));
                ViewBag.Modules = responseData;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
        public void GetUsers()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _user.GetUsers();
                responseData = JsonConvert.SerializeObject(new List<UserDetailsModel>(result.ResponseBody));
                ViewBag.Users = responseData;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
        [HttpPost]
        public string AddUserModulePermission(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;

                var getData = JsonConvert.DeserializeObject<UserModulePermissionModel>(data.ToString());
                var result = _UserModulePermission.CreatePermission(getData);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of UserModulePermission Controller.");
            }
            return responseData;
        }
        [HttpPut]
        public string UpdateUserModulePermission(object data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<List<UserModulePermissionModel>>(data.ToString());
                var result = _UserModulePermission.UpdatePermission(getData);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of UserModulePermission Controller.");
            }
            return response;
        }
        [HttpGet]
        public string GetDetails(object data)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _UserModulePermission.GetPermissions();
                responseData = JsonConvert.SerializeObject(new List<UserModulePermissionModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of UserModulePermissionDetails Controller.");
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
                ViewBag.UserInfo = UserInfo;
                var result = _UserModulePermission.GetPermissionByUserId(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of UserModulePermissionDetails Controller.");
            }
            return responseData;
        }
        [HttpDelete]
        public string DeleteDetails(int id)
        {
            var response = new HttpResponseMessage();
            string responseData = String.Empty;

            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _UserModulePermission.DeletePermissions(id);
                responseData = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of UserModulePermissionDetails Controller.");
            }
            return responseData;
        }

    }
}
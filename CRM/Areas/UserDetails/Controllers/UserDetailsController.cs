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
using System.EnterpriseServices;
using Customer.Data.Application;
using System.Web.Http.Results;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CRM.Areas.UserDetails.Controllers
{
    //[MyCustomActionFiler]
    //[HandleError()]
    public class UserDetailsController : BaseController
    {
        private readonly IUser _user;
        private readonly IDepartment _department;
        private readonly IRepresentatives _representatives;
        public UserDetailsController(IUser user, IDepartment department, IRepresentatives representatives)
        {
            _user = user;
            _department = department;
            _representatives = representatives;
        }
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            GetDepartments();
            GetRepresentatives();
            return View();
        }
        public void GetDepartments()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _department.GetDepartments();
                responseData = JsonConvert.SerializeObject(new List<DepartmentModel>(result.ResponseBody));
                ViewBag.Department = responseData;
                logger.Info(response);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
        public void GetRepresentatives()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _representatives.GetRepresentatives();
                responseData = JsonConvert.SerializeObject(new List<RepresentativesModel>(result.ResponseBody));
                ViewBag.Representatives = responseData;
                logger.Info(response);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
        [HttpPost]
        public string AddUserDetails(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {

                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<UserDetailsModel>(data.ToString());
                var result = _user.CreateUser(getData);
                responseData = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of UserDetails Controller.");
            }
            return responseData;
        }
        [HttpPut]
        public string UpdateUserDetails(object data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<UserDetailsModel>(data.ToString());
                var result = _user.UpdateUser(getData);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of UserDetails Controller.");
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
                var result = _user.GetUsers();
                responseData = JsonConvert.SerializeObject(new List<UserDetailsModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of UserDetailsDetails Controller.");
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
                var result = _user.GetUserById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of UserDetailsDetails Controller.");
            }
            return responseData;
        }
        [HttpDelete]
        public string DeleteDetails(int id)
        {
            string response = String.Empty;

            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _user.DeleteUser(id);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of UserDetailsDetails Controller.");
            }
            return response;
        }
    }
}
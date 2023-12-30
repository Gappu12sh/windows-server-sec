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

namespace CRM.Areas.Department.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class DepartmentController : BaseController
    {
        private readonly IDepartment _department;

        public DepartmentController(IDepartment department)
        {
            _department= department;
        }

        // GET: Department/Department
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
            return View();
        }
       
        [HttpPost]
        public string AddDepartment(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<DepartmentModel>(data.ToString());
                var result = _department.CreateDepartment(getData);
                responseData = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of Department Controller.");
            }
            return responseData;
        }
        [HttpPut]
        public string UpdateDepartment(object data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<DepartmentModel>(data.ToString());
                var result = _department.UpdateDepartment(getData);
                response = JsonConvert.SerializeObject(result);                
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Department Controller.");
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
                ViewBag.UserInfo = UserInfo;
                var result = _department.GetDepartments();
                responseData = JsonConvert.SerializeObject(new List<DepartmentModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of DepartmentDetails Controller.");
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
                var result = _department.GetDepartmentById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of DepartmentDetails Controller.");
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
                var result = _department.DeleteDepartment(id);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of DepartmentDetails Controller.");
            }
            return response;
        }
    }
}
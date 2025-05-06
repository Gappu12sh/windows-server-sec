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
using CRM.SendBoxSMSService;

namespace CRM.Areas.Representatives.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class RepresentativesController : BaseController
    {
        private readonly IRepresentatives _representatives;
        private readonly IParty _party;

        public RepresentativesController(IRepresentatives representatives,IParty party)
        {
            _representatives = representatives;
            _party = party;
        }

        // GET: Representatives/Representatives
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
            return View();
        }
        public string GetPartyByRepId(int id)
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _party.GetPartyByRepId(id);
                responseData = JsonConvert.SerializeObject(new List<PartyMasterModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetPartyByRepId function of Representatives Controller.");
            }
            return responseData;
        }
        //[HttpPost]
        //public string AddRepresentatives(object data)
        //{
        //    //var response = new HttpResponseMessage();
        //    string responseData = String.Empty;
        //    try
        //    {
        //        ViewBag.UserInfo = UserInfo;
        //        var getData = JsonConvert.DeserializeObject<RepresentativesModel>(data.ToString());
        //        var result = _representatives.CreateRepresentative(getData);
        //        responseData = JsonConvert.SerializeObject(result);                           
        //    }
        //    catch (Exception ex)
        //    {
        //        responseData = ex.Message;
        //        logger.Error("Error in AddEditDelete function of Representatives Controller.");
        //    }
        //    return responseData;
        //}






        [HttpPost]
        public string AddRepresentatives(object data)
        {
            string responseData = String.Empty;
            try
            {
                // Deserialize the incoming data
                var getData = JsonConvert.DeserializeObject<RepresentativesModel>(data.ToString());

                // Optionally validate Rep_Code if necessary
                if (string.IsNullOrEmpty(getData.Rep_Code))
                {
                    responseData = JsonConvert.SerializeObject(new { Error = "Representative code is required." });
                    return responseData;
                }

                // Call your business logic layer to create the representative
                var result = _representatives.CreateRepresentative(getData);

                // Serialize the result and return it
                responseData = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddRepresentatives function of Representatives Controller.");
            }
            return responseData;
        }










        [HttpPut]
        public string UpdateRepresentatives(object data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<RepresentativesModel>(data.ToString());
                var result = _representatives.UpdateRepresentative(getData);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Representatives Controller.");
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
                var result = _representatives.GetRepresentatives();
                responseData = JsonConvert.SerializeObject(new List<RepresentativesModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of RepresentativesDetails Controller.");
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
                var result = _representatives.GetRepresentativeById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of RepresentativesDetails Controller.");
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
                var result = _representatives.DeleteRepresentative(id);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of RepresentativesDetails Controller.");
            }
            return response;
        }
    }
}
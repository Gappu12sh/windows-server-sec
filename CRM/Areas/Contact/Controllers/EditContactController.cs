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

namespace CRM.Areas.Contact.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class EditContactController : BaseController
    {
        private readonly IParty _party;
        private readonly IRepresentatives _representatives;
        private readonly ILookup _lookup;

        public EditContactController(IParty party, IRepresentatives representatives, ILookup lookup)
        {
            _party = party;
            _representatives = representatives;
            _lookup = lookup;
        }

        // GET: Contact/Contact
        public ActionResult Index(string id)
        {
            string str = Request.QueryString["id"].ToString();
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
            GetLookup();
            GetRepresentatives();
            ViewBag.PartyId = Request.QueryString["id"].ToString();            
            return View();
        }
        public void GetLookup()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _lookup.GetLookup();
                responseData = JsonConvert.SerializeObject(new List<LookupModel>(result.ResponseBody));
                ViewBag.Lookup = responseData;
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
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
        [HttpPut]
        public string UpdateParty(object data)
        {
            string response = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<PartyMasterModel>(data.ToString());
                var result = _party.UpdateParty(getData);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Department Controller.");
            }
            return response;
        }
        [HttpPut]
        public string UpdatePartyAddress(object data)
        {
            string response = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<AdditionalPartyAddressModel>(data.ToString());
                var result = _party.UpdatePartyAddress(getData);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Department Controller.");
            }
            return response;
        }
        [HttpPut]
        public string UpdateContact(object data)
        {
            string response = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<ContactModel>(data.ToString());
                var result = _party.UpdateContact(getData);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Department Controller.");
            }
            return response;
        }
        [HttpGet]
        public string GetDetailsById(int id)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _party.GetPartyById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of Edit Contact Details Controller.");
            }
            return responseData;
        }

    }
}
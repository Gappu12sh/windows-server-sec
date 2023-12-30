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
    public class ContactDetailsController : BaseController
    {
        private readonly IParty _party;
        private readonly IRepresentatives _representatives;
        private readonly ILookup _lookup;

        public ContactDetailsController(IParty party, IRepresentatives representatives, ILookup lookup)
        {
            _representatives = representatives;
            _lookup = lookup;
            _party = party;
        }

        // GET: Contact/ContactDetails
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
            GetLookup();
            GetRepresentatives();
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

        [HttpGet]
        public string GetDetails(object data)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _party.GetParty();
                responseData = JsonConvert.SerializeObject(new List<PartyMasterModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ContactDetails Controller.");
            }
            return responseData;
        }
    }
}
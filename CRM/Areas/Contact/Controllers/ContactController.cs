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
using Customer.Model;
using System.Net.Http;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Org.BouncyCastle.Asn1.Ocsp;

namespace CRM.Areas.Contact.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class ContactController : BaseController
    {
        private readonly IParty _party;
        private readonly IRepresentatives _representatives;
        private readonly ILookup _lookup;

        public ContactController(IParty party, IRepresentatives representatives, ILookup lookup)
        {
            _party = party;
            _representatives = representatives;
            _lookup = lookup;
        }

        // GET: Contact/Contact
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
        [HttpPost]
        public string AddParty(object data)
        {
            string response = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<PartyMasterModel>(data.ToString());
                var result = _party.CreateParty(getData);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in Add function of Contact Controller.");
            }
            return response;
        }
        [HttpPost]
        public string AddAddressByParty(object data)
        {
            string response = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<AdditionalPartyAddressModel>(data.ToString());
                var result = _party.CreateAddressByParty(getData);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in Add function of Contact Controller.");
            }
            return response;
        }
        [HttpPost]
        public string AddContactByParty(object data)
        {
            string response = String.Empty;
            try
            {
                var getData = JsonConvert.DeserializeObject<ContactModel>(data.ToString());
                var result = _party.CreateContactByParty(getData);
                response = JsonConvert.SerializeObject(result);
                ViewBag.UserInfo = UserInfo;
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in Add function of Contact Controller.");
            }
            return response;
        }




        //[HttpPost]
        //public JsonResult AddLookup(LookupModel data)
        //{
        //    try
        //    {
        //        var result = _lookup.CreateLookup(data);
        //        return Json(new { HasError = false, Data = result }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error in AddLookup function of Contact Controller.", ex);
        //        return Json(new { HasError = true, Message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}






    }

}
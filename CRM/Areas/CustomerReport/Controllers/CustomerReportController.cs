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

namespace CRM.Areas.CustomerReport.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class CustomerReportController : BaseController
    {
        private readonly IParty _party;

        public CustomerReportController(IParty party)
        {
            _party = party;
        }

        // GET: CustomerReport/CustomerReport
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            GetParties();
            return View();
        }
        public string GetParties()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _party.GetParty();
                responseData = JsonConvert.SerializeObject(new List<PartyMasterModel>(result.ResponseBody));
                ViewBag.Companies = responseData;
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
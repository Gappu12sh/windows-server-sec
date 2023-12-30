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

namespace CRM.Areas.Samples.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class SamplesDetailsController : BaseController
    {
        private readonly IQuotation _quotation;

        public SamplesDetailsController(IQuotation quotation)
        {
            _quotation = quotation;
        }

        // GET: Samples/SamplesDetails
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            return View();
        }       

        [HttpGet]
        public string GetDetails(object data)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _quotation.GetQuotationList();
                responseData = JsonConvert.SerializeObject(new List<QuotationMasterModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of SamplesDetails Controller.");
            }
            return responseData;
        }
    }
}
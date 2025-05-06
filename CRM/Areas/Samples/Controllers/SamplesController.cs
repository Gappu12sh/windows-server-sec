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

namespace CRM.Areas.Samples.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class SamplesController : BaseController
    {
        private readonly IQuotation _quotation;
        private readonly IProduct _product;
        private readonly IApplicationUsage _applicationUsage;
        private readonly ILookup _lookup;
        private readonly IParty _party;

        public SamplesController(IQuotation quotation, IProduct product, IApplicationUsage applicationUsage, ILookup lookup, IParty party)
        {
            _quotation = quotation;
            _product = product;
            _applicationUsage = applicationUsage;
            _lookup = lookup;
            _party = party;
        }

        // GET: Samples/Samples
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            GetLookup();
            GetProduct();
            GetCompany();
            GetApplicationUsage();
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
        public string GetProduct()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _product.GetProduct();
                responseData = JsonConvert.SerializeObject(new List<ProductModel>(result.ResponseBody));
                ViewBag.Product = responseData;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of Sample Controller.");
            }
            return responseData;
        }
        public void GetCompany()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _party.GetParty();
                responseData = JsonConvert.SerializeObject(new List<PartyMasterModel>(result.ResponseBody));
                ViewBag.Company = responseData;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }

        public void GetApplicationUsage()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _applicationUsage.GetApplicationUsage();
                responseData = JsonConvert.SerializeObject(new List<ApplicationUsageModel>(result.ResponseBody));
                ViewBag.ApplicationUsage = responseData;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
        [HttpPost]
        public string AddSamples(string data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<QuotationMasterModel>(data.ToString());
                var result = _quotation.CreateQuotation(getData);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in Add function of Samples Controller.");
            }
            return response;
        }

        [HttpPost]
        public string AddSamplesProduct(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;

                var getData = JsonConvert.DeserializeObject<ProductModel>(data.ToString());
                var result = _product.CreateProduct(getData);
                responseData = JsonConvert.SerializeObject(result);
                if (result.ErrorCodes == null)
                {
                    responseData = GetProduct();
                }

            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of Product Controller.");
            }
            return responseData;
        }


        [HttpGet]
        public string GetQuotationBySampleName(string sampleName,int partyId)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _quotation.GetQuotationBySampleName(sampleName, partyId);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of Edit Samples Details Controller.");
            }
            return responseData;
        }
    }

}
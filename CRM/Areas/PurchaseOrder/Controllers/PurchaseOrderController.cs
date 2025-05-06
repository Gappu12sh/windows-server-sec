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

namespace CRM.Areas.PurchaseOrder.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class PurchaseOrderController : BaseController
    {
        private readonly IQuotation _quotation;
        private readonly IProduct _product;
        private readonly IPurchaseOrder _purchaseOrder;
        private readonly IApplicationUsage _applicationUsage;
        private readonly ILookup _lookup;
        private readonly IParty _party;

        public PurchaseOrderController(IQuotation quotation, IProduct product, IApplicationUsage applicationUsage, ILookup lookup, IParty party,IPurchaseOrder purchaseOrder)
        {
            _quotation = quotation;
            _product = product;
            _applicationUsage = applicationUsage;
            _lookup = lookup;
            _party = party;
            _purchaseOrder = purchaseOrder;
        }

        // GET: PurchaseOrder/PurchaseOrder
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            GetCompany();
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

        [HttpPost]
        public string AddPurchaseOrder(string data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<PurchaseOrderModel>(data.ToString());
                var result = _purchaseOrder.CreatePurchaseOrder(getData);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in Add function of PurchaseOrder Controller.");
            }
            return response;
        }

        [HttpGet]
        public string GetQuotationByPartyId(int partyId)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _quotation.GetQuotationListByParty(partyId);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of Edit PurchaseOrder Details Controller.");
            }
            return responseData;
        }
        [HttpGet]
        public string GetLastPurchaseOrderId()
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _purchaseOrder.GetLastPurchaseOrderId();
                responseData = result.ToString();
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of Edit PurchaseOrder Details Controller.");
            }
            return responseData;
        }

        [HttpGet]
        public JsonResult CheckPONumberExists(string poNumber)
        {
            try
            {
                bool exists = _purchaseOrder.CheckPONumberExists(poNumber);
                return Json(new { exists }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error("Error in CheckPONumberExists: " + ex.Message);
                return Json(new { exists = false }, JsonRequestBehavior.AllowGet);
            }
        }



    }

}
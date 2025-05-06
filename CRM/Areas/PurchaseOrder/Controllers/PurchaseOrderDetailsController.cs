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

namespace CRM.Areas.PurchaseOrder.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class PurchaseOrderDetailsController : BaseController
    {
        private readonly IPurchaseOrder _purchaseOrder;

        public PurchaseOrderDetailsController(IPurchaseOrder PurchaseOrder)
        {
            _purchaseOrder = PurchaseOrder;
        }

        // GET: PurchaseOrder/PurchaseOrderDetails
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
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
                var result = _purchaseOrder.GetPurchaseOrder();
                responseData = JsonConvert.SerializeObject(new List<PurchaseOrderModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of PurchaseOrderDetails Controller.");
            }
            return responseData;
        }
        [HttpGet]
        public string GetDetailsById(int id)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _purchaseOrder.GetPurchaseOrderById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of Edit PurchaseOrder Details Controller.");
            }
            return responseData;
        }
    }
}
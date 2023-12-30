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
using Org.BouncyCastle.Asn1.Ocsp;

namespace CRM.Areas.Product.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class ProductController : BaseController
    {
        private readonly IProduct _product;
        private readonly IApplicationUsage _applicationUsage;

        public ProductController(IProduct product, IApplicationUsage applicationUsage)
        {
            _product = product;
            _applicationUsage = applicationUsage;
        }

        // GET: Product/Product
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
            GetApplicationUsage();
            return View();
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
        public string AddProduct(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;

                var getData = JsonConvert.DeserializeObject<ProductModel>(data.ToString());
                var result = _product.CreateProduct(getData);
                responseData = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of Product Controller.");
            }
            return responseData;
        }
        [HttpPut]
        public string UpdateProduct(object data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<ProductModel>(data.ToString());
                var result = _product.UpdateProduct(getData);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Product Controller.");
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
                var result = _product.GetProduct();
                responseData = JsonConvert.SerializeObject(new List<ProductModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ProductDetails Controller.");
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
                var result = _product.GetProductById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ProductDetails Controller.");
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
                var result = _product.DeleteProduct(id);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of ProductDetails Controller.");
            }
            return response;
        }
    }
}
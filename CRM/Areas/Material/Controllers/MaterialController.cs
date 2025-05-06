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

namespace CRM.Areas.Material.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class MaterialController : BaseController
    {
        private readonly IMaterial _material;
        private readonly IProduct _product;
        private readonly IParty _party;

        public MaterialController(IMaterial material, IProduct product, IParty party)
        {
            _material = material;
            _product = product;
            _party = party;
        }

        // GET: Material/Material
        public ActionResult Index()
        {
            ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.UserPagePermissionDetails = JsonConvert.SerializeObject(UserPagePermissionDetails);
            GetParty();
            GetProduct();
            return View();
        }
        public void GetParty()
        {
            var data = new object();
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _party.GetParty();
                responseData = JsonConvert.SerializeObject(new List<PartyMasterModel>(result.ResponseBody));
                ViewBag.Party = responseData;
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
        public void GetProduct()
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
                logger.Error("Error in GetDetails function of ApplicationUsageDetails Controller.");
            }
        }
       
        [HttpPost]
        public string AddMaterial(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<MaterialModel>(data.ToString());
                var result = _material.CreateMaterial(getData);
                responseData = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of Material Controller.");
            }
            return responseData;
        }
        [HttpPut]
        public string UpdateMaterial(object data)
        {
            string response = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<MaterialModel>(data.ToString());
                var result = _material.UpdateMaterial(getData);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in AddEditDelete function of Material Controller.");
            }
            return response;
        }
        [HttpGet]
        public string GetDetails(object data)
        {
            var response = new HttpResponseMessage();
            var responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _material.GetMaterials();
                responseData = JsonConvert.SerializeObject(new List<MaterialModel>(result.ResponseBody));
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of MaterialDetails Controller.");
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
                var result = _material.GetMaterialById(id);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of MaterialDetails Controller.");
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
                var result = _material.DeleteMaterial(id);
                response = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                logger.Error("Error in GetDetails function of MaterialDetails Controller.");
            }
            return response;
        }
        [HttpPost]
        public string AddNewRate(object data)
        {
            //var response = new HttpResponseMessage();
            string responseData = String.Empty;
            try
            {
                ViewBag.UserInfo = UserInfo;
                var getData = JsonConvert.DeserializeObject<MaterialRatesModel>(data.ToString());
                var result = _material.AddNewRate(getData);
                responseData = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                responseData = ex.Message;
                logger.Error("Error in AddEditDelete function of Material Controller.");
            }
            return responseData;
        }
        [HttpGet]
        public string GetSampleProductDetails(int partyId)
        {
            var response = new HttpResponseMessage();
            string responseData = String.Empty;

            try
            {
                ViewBag.UserInfo = UserInfo;
                var result = _material.GetSampleProductDetails(partyId);
                responseData = JsonConvert.SerializeObject(result.ResponseBody);
            }
            catch (Exception ex)
            {
                //response = ex.Message;
                logger.Error("Error in GetDetails function of MaterialDetails Controller.");
            }
            return responseData;
        }
    }
}
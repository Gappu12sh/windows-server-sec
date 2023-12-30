using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Web.Mvc;

namespace CRM.Areas.User.Controllers
{
    [MyCustomActionFiler]
    [HandleError()]
    public class ResetPasswordController : Base1Controller
    {
        public ActionResult Index()
        {
            //ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            return View();
        }

        //[HttpPost]
        //public string ResetPassword(Reset_Password resetPassword)
        //{
        //    string response = string.Empty;
        //    try
        //    {
        //        resetPassword.NewPassword = Commonfunction.Encryptdata(Convert.ToString(resetPassword.NewPassword));
        //        resetPassword.OldPassword = Commonfunction.Encryptdata(Convert.ToString(resetPassword.OldPassword));

        //        string url = webAPIPort + "/User/ResetPassword";
        //        string dataToPost = JsonConvert.SerializeObject(resetPassword);
        //        response = Commonfunction.PostHttpWebRequestAPICall(url, dataToPost);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Exception in App_ResetPasswordController: ResetPassword" + ex.ToString());
        //        response = ex.Message;
        //    }
        //    return response;
        //}
    }
}

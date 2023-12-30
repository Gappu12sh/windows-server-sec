using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using System.Net;
using System.Configuration;

using Newtonsoft.Json;

namespace CRM
{
    public class MyCustomActionFiler : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.HttpContext.Session["UserDetails"] == null)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Account");
                redirectTargetDictionary.Add("area", "User");
                actionContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }
            else
            {
                //UserSetup userSetup = (UserSetup)actionContext.HttpContext.Session["UserDetails"];
                //string controller = actionContext.Controller.ToString();
                //if (userSetup.ResponseCode == (int)LoginResponseCode.Success && userSetup.IsNewPassword && controller != "CRM.Areas.User.Controllers.ResetPasswordController")
                //{
                //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                //    redirectTargetDictionary.Add("action", "Index");
                //    redirectTargetDictionary.Add("controller", "ResetPassword");
                //    redirectTargetDictionary.Add("area", "User");
                //    actionContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                //}
            }


            base.OnActionExecuting(actionContext);
        }

    }
    public class MyCustomErrorHandler : HandleErrorAttribute
    {
        internal static readonly ILog logger = LogManager.GetLogger(typeof(BaseController));
        public override void OnException(ExceptionContext filterContext)
        {
            logger.Error(string.Format("Error Occured: Controller: {0} Exception Message : {1}, Exception Stack Trace :{2}", filterContext.Controller.ToString(), filterContext.Exception.Message, filterContext.Exception.StackTrace));
        }
    }

    public class MyCustomActionHandler : ActionFilterAttribute
    {
        internal static readonly ILog logger = LogManager.GetLogger(typeof(BaseController));
        internal static readonly ILog spLogger = LogManager.GetLogger("SpecialLogger");

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string machineName = string.Empty;
            string requestController = Convert.ToString(filterContext.RouteData.Values["controller"]);
            if ((Dictionary<int, string>)filterContext.Controller.ViewBag.UserPermissionDetails != null)
            {
                string action = Convert.ToString(filterContext.RouteData.Values["action"]);

                string myIP = "";
                string hostName = "";
                try
                {
                    //myIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                    System.Web.HttpContext context = System.Web.HttpContext.Current;
                    myIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (myIP == "" || myIP == null)
                        myIP = context.Request.ServerVariables["REMOTE_ADDR"];
                    hostName = Dns.GetHostEntry(Dns.GetHostName()).HostName.ToString();
                    machineName = myIP;
                }
                catch (Exception ex)
                {
                    logger.Error("Exception Occured at MyCustomActionFilter at Login Action for getting IP and machine Name" + ex.Message);

                }
                //GlobalContext.Properties["TaskType"] = requestController + "/" + action;
                //GlobalContext.Properties["UserId"] = ((UserSetup)filterContext.Controller.ViewBag.UserInfo).UserId;
                //GlobalContext.Properties["SystemIP"] = myIP;
                //GlobalContext.Properties["SytemDetails"] = hostName;

                //// Dont delete this logger entry. This is to maintain entry in [Jade$Manifest User Setup Log]  // Nikhil 
                //spLogger.Info("Log for user Tracing details");
                Dictionary<int, string> permDet = new Dictionary<int, string>();
                permDet = (Dictionary<int, string>)filterContext.Controller.ViewBag.UserPermissionDetails;
                //if (permDet.ContainsValue(requestController))
                if (1==1)
                {
                    // update Last updated time 
                    string webAPIPort = Convert.ToString(ConfigurationManager.AppSettings["WebAPIPort"]);
                    string response = string.Empty;
                   // string userId = ((UserSetup)filterContext.Controller.ViewBag.UserInfo).Emp_ID;

                    //string url = webAPIPort + "/User/UpdateLastActivity";
                    //UsersDetail userDetail = new UsersDetail();
                    //userDetail.UserId = userId;
                    //Uri uri = new Uri(url);


                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers["Content-Type"] = "application/json";
                        //wc.UploadStringAsync(uri, "POST", JsonConvert.SerializeObject(userDetail));
                    }

                    //MessageResponse objResponse = new MessageResponse();
                    //objResponse = JsonConvert.DeserializeObject<MessageResponse>(response);
                    //if (objResponse.ResponseCode != (int)ResponseCode.Success)
                    //{
                    //    logger.Error("Error in updating last activity time" + objResponse.ResponseMessage);
                    //}
                    //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    //    redirectTargetDictionary.Add("action", "Login");
                    //    redirectTargetDictionary.Add("controller", "Account");
                    //    redirectTargetDictionary.Add("area", "User");
                    //    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                    //    filterContext.Controller.TempData["error"] = "You are not authorised to access " + requestController + " Page.";
                    //}
                }
                else
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", "Welcome");
                    redirectTargetDictionary.Add("controller", "Account");
                    redirectTargetDictionary.Add("area", "User");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                    filterContext.Controller.TempData["error"] = "You are not authorised to access " + requestController + " Page.";
                }
                //var v = filterContext.Controller.ViewBag.UserInfo.GetType().GetProperty(requestController).GetValue(filterContext.Controller.ViewBag.UserInfo, null);
                //if (!v)
                //{
                //    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                //    redirectTargetDictionary.Add("action", "WelcomePage");
                //    redirectTargetDictionary.Add("controller", "Account");
                //    redirectTargetDictionary.Add("area", "User");
                //    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                //    filterContext.Controller.TempData["error"] = "You are not authorised to access " + requestController + " Page.";
                //}
                //else
                //{
                //    //filterContext.Controller.ViewBag.error = null;
                //    string webAPIPort = Convert.ToString(ConfigurationManager.AppSettings["WebAPIPort"]);
                //    string response = string.Empty;
                //    string userId = Convert.ToString(((UserSetup)filterContext.Controller.ViewBag.UserInfo).UserId);
                //    string LoginController = ConfigurationManager.AppSettings["UpdateLastActivityController"].ToString();
                //    string url = webAPIPort + "/" + LoginController + "?UserID=" + userId + "&MachineName=" + machineName;

                //    using (WebClient wc = new WebClient())
                //    {
                //        response = wc.DownloadString(url);
                //    }

                //    Response objResponse = new Response();
                //    objResponse = JsonConvert.DeserializeObject<Response>(response);
                //    if (objResponse.DbStatus != 1)
                //    {
                //        RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                //        redirectTargetDictionary.Add("action", "Login");
                //        redirectTargetDictionary.Add("controller", "Account");
                //        redirectTargetDictionary.Add("area", "");
                //        filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                //        filterContext.Controller.TempData["error"] = "You are not authorised to access " + requestController + " Page.";
                //    }
                //}
            }
        }
    }

    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Flush();

            //convert the current filter context to file and get the file path
            string filePath = (filterContext.Result as FilePathResult).FileName;

            //delete the file after download
            System.IO.File.Delete(filePath);
        }
    }
}


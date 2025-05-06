using Microsoft.Web.WebPages.OAuth;
using Newtonsoft.Json;
using CRM.Areas.User.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using Customer.Model;
using AccountModel = CRM.Areas.User.Models;
using Customer.BusinessLogic.Utilities;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;
using System.Drawing;
using Customer.Model.Utilities;

namespace CRM.Areas.User.Controllers
{
    public class AccountController : Base1Controller
    {
        private readonly ILogin _login;
        private readonly IUserModulePermission _permission;
        public AccountController(ILogin login, IUserModulePermission permission)
        {
            _login = login;
            _permission = permission;
        }
        public ActionResult Index()
        {
            return View();
        }
        [MyCustomActionFiler]
        [HandleError()]
        public ActionResult Welcome()
        {
            //ViewBag.UserInfo = UserInfo;
            ViewBag.UserPermissionDetails = UserPermissionDetails;
            ViewBag.error = TempData["error"];
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HandleError()]
        public ActionResult Logout()
        {
            //UserSetup objUser = new UserSetup();
            //if (Session["UserDetails"] != null)
            //    objUser = (UserSetup)Session["UserDetails"];
            //string loginController = "User/LogoutUser";
            //string url = webAPIPort + "/" + loginController + "?UserID=" + objUser.UserId;
            //string response = Commonfunction.GetHttpWebRequestAPICall(url);
            string url = "~/User/Account/Login";
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return Redirect(url);
        }
        //
        // POST: /Account/Login

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(AccountModel.LoginModel model, string returnUrl)
        //{

        //    if (ModelState.IsValid)
        //    {

        //        string loginController = Convert.ToString(ConfigurationManager.AppSettings["LoginController"]);


        //        var userLogin = new UserLoginModel
        //        {
        //            UserName = model.UserName,
        //            Password = model.Password
        //        };
        //        //var getToken = _login.GetToken(userLogin);
        //        //var response = _login.GetUserLoginData(new UserTokenModel { Token = getToken.ResponseBody });
        //        var response = _login.GetUserLoginData(userLogin);
        //        if (!response.HasError())
        //        {
        //            //var resp = JsonConvert.DeserializeObject<UserLoginDataModel>(response.Content.ReadAsStringAsync().Result);

        //            //UserSetup objUser = JsonConvert.DeserializeObject<UserSetup>(response);
        //            var data = response.ResponseBody;
        //            Session["UserId"] = data.UserId;
        //            Session["UserName"] = data.UserName;
        //            Session["Email"] = data.Email;
        //            Session["AccessToken"] = data.AccessToken;
        //            //if (objUser.ResponseCode == (int)ResponseCode.Success)
        //            //{
        //            Session["UserDetails"] = data;
        //            ViewBag.UserInfo = data;
        //            LoadUserPermission(data.UserId);
        //            //if (UpdateLoginStatus(loginDetails))
        //            //{
        //            //if(objUser.RoleId==2 || objUser.RoleId == 3 || objUser.RoleId == 12)
        //            //{
        //            //    return RedirectToAction("Index", "MerchantTna", new { area = "MerchantTna" });
        //            //}
        //            //else if (objUser.RoleId == 4)
        //            //{
        //            //    return RedirectToAction("Index", "FabricTna", new { area = "FabricTna" });
        //            //}
        //            //else if (objUser.RoleId == 6)
        //            //{
        //            //    return RedirectToAction("Index", "TrimsTna", new { area = "TrimsTna" });
        //            //}
        //            //else if (objUser.RoleId == 9)
        //            //{
        //            //    return RedirectToAction("Index", "DirectorDashboard", new { area = "DirectorDashboard" });
        //            //}
        //            //else
        //            //{
        //            return RedirectToAction("Welcome", "Account");
        //            //}
        //            //}

        //            //}
        //            //else if (objUser.ResponseCode == (int)LoginResponseCode.PasswordChangeRequired)
        //            //{
        //            //    return RedirectToAction("Index", "ResetPassword");
        //            //}
        //            //else if (objUser.ResponseCode == (int)LoginResponseCode.Exception && objUser.ResponseMessage.Trim().Length > 0)
        //            //{
        //            //    ViewBag.Message = objUser.ResponseMessage.Trim();
        //            //    //logg
        //            //}
        //            //else
        //            //{
        //            //    ViewBag.Message = objUser.ResponseMessage.Trim();
        //            //}
        //        }

        //    }
        //    else
        //    {
        //        // If we got this far, something failed, redisplay form
        //        ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //    }
        //    return View(model);
        //}


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountModel.LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string loginController = Convert.ToString(ConfigurationManager.AppSettings["LoginController"]);

                var userLogin = new UserLoginModel
                {
                    UserName = model.UserName,
                    Password = model.Password
                };

                // Get the login response from the login manager
                var response = _login.GetUserLoginData(userLogin);

                // Check for any errors in the response
                if (response.HasError())
                {
                    // Add a custom error message to ModelState
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    return View(model);
                }

                // If login is successful, set session variables
                var data = response.ResponseBody;
                Session["UserId"] = data.UserId;
                Session["UserName"] = data.UserName;
                Session["Email"] = data.Email;
                Session["AccessToken"] = data.AccessToken;
                Session["UserDetails"] = data;

                // Optionally, load user permissions and redirect to the welcome page
                LoadUserPermission(data.UserId);

                // Redirect to the Welcome page (or a different page based on role, if needed)
                return RedirectToAction("Welcome", "Account");
            }
            else
            {
                // If we got this far, something failed, redisplay the form with the error message
                ModelState.AddModelError("", "Please correct the errors and try again.");
            }

            return View(model);
        }











        //private bool UpdateLoginStatus(LoginDetails loginDetails)
        //{
        //    string url = webAPIPort + "/User/UpdateLoginStatus";
        //    //string dataToPost = JsonConvert.SerializeObject(userId);

        //    string response = Commonfunction.PostHttpWebRequestAPICall(url, JsonConvert.SerializeObject(loginDetails));


        //    if (response != string.Empty)
        //    {
        //        MessageResponse messageResponse = JsonConvert.DeserializeObject<MessageResponse>(response);
        //        if (messageResponse.ResponseCode != (int)ResponseCode.Success)
        //        {
        //            ViewBag.Message = messageResponse.ResponseMessage;
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "There is some technical issue. Please try again later.";
        //        return false;
        //    }
        //    return true;

        //}

        //internal void LoadUserPermission(string userId)
        //{
        //    UserPermissionDetails = null;

        //    string url = webAPIPort + "/User/GetUserPermission/?userId=" + userId;
        //    //string dataToPost = JsonConvert.SerializeObject(userId);
        //    string response = Commonfunction.GetHttpWebRequestAPICall(url);
        //    if (response != string.Empty)
        //    {
        //        UserPermission userPermission = JsonConvert.DeserializeObject<UserPermission>(response);
        //        if (userPermission.ResponseCode == (int)ResponseCode.Success)
        //        {
        //            //UserPermissionDetails = userPermission.UserPermissionDetails;

        //            Dictionary<string, string> permDet = new Dictionary<string, string>();

        //            foreach (UserPermissionDetail item in userPermission.UserPermissionDetails)
        //            {
        //                if (!permDet.ContainsKey(item.ControllerName))
        //                    permDet.Add(item.ControllerName, item.PageName);
        //            }
        //            UserPermissionDetails = permDet;


        //            //for page permission
        //            Dictionary<string, string> pagepermDet = new Dictionary<string, string>();

        //            foreach (UserPagePermissionDetail item in userPermission.UserPagePermissionDetails)
        //            {
        //                //if (!pagepermDet.ContainsKey(item.PageName))
        //                pagepermDet.Add(item.PageName + item.ActionName, item.ActionName);
        //            }
        //            UserPagePermissionDetails = pagepermDet;

        //        }
        //    }
        //}

        //
        // POST: /Account/LogOff

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult LogOff()
        //{
        //    WebSecurity.Logout();

        //    return RedirectToAction("Index", "Home");
        //}

        //
        // GET: /Account/Register

        //[AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        //
        // POST: /Account/Disassociate
        internal void LoadUserPermission(int userId)
        {
            UserPermissionDetails = null;
            string responseData = String.Empty;
            var result = _permission.GetModuleByUserId(userId);
            var permissionData = result.ResponseBody;
            responseData = JsonConvert.SerializeObject(result.ResponseBody);
            Dictionary<int, string> permission = new Dictionary<int, string>();
            foreach (var item in permissionData)
            {
                if (!permission.ContainsKey(item.ModuleId.Value))
                    permission.Add(item.ModuleId.Value, item.module.ModuleName);
            }
            UserPermissionDetails = permission;

            var modules = _permission.GetModules();
            var moduleData = modules.ResponseBody;
            Dictionary<string, PermissionByUser> pagepermDet = new Dictionary<string, PermissionByUser>();
            foreach (var module in moduleData)
            {
                var permissionByModule = permissionData.FirstOrDefault(x => x.ModuleId == module.ModuleId);
                if (permissionByModule != null)
                {
                    var permissionByUser = new PermissionByUser();
                    foreach (var item in permissionByModule.userPermissionModels)
                    {                        
                        permissionByUser.UserId = permissionByModule.UserId.Value;
                        switch (item.Actions.Code)
                        {
                            case "MODULE-ACTION-ADD":
                                permissionByUser.IsAdd = item.IsActive;
                                break;
                            case "MODULE-ACTION-EDIT":
                                permissionByUser.IsEdit = item.IsActive;
                                break;
                            case "MODULE-ACTION-VIEW":
                                permissionByUser.IsView = item.IsActive;
                                break;
                            case "MODULE-ACTION-DELETE":
                                permissionByUser.IsDeleted = item.IsActive;
                                break;
                            case "MODULE-ACTION-ADD-ADDRESS":
                                permissionByUser.IsAddAddress = item.IsActive;
                                break;
                            case "MODULE-ACTION-VIEW-PARTIES":
                                permissionByUser.IsViewParty = item.IsActive;
                                break;
                            case "MODULE-ACTION-ADD-RATE":
                                permissionByUser.IsAddRate = item.IsActive;
                                break;
                        }
                    }
                    pagepermDet.Add(module.ModuleName, (permissionByUser));
                }
            }

            UserPagePermissionDetails = pagepermDet;
        }




        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        //[AllowAnonymous]
        //[MyCustomActionHandler]
        //public ActionResult ChangePassword(string returnUrl)
        //{
        //    if (Session["UserDetails"] != null)
        //    {
        //        UserSetup objUser = (UserSetup)Session["UserDetails"];
        //        ViewBag.UserInfo = objUser;
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}




        public ActionResult Wellcome()
        {
            return View();
        }
    }
}

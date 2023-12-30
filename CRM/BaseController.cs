using System.Web.Mvc;
using log4net;
using System.Configuration;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Customer.Model;
using Customer.BusinessLogic.Interfaces;

namespace CRM
{
    [MyCustomActionHandler]
    public class BaseController : Base1Controller
    {       
    }

    [MyCustomErrorHandler]
    [HandleError()]
    public class Base1Controller : Controller
    {
        
        internal static readonly ILog logger = LogManager.GetLogger(typeof(BaseController));
        internal string webAPIPort = Convert.ToString(ConfigurationManager.AppSettings["WebAPIPort"]);
        internal string OMSwebAPIPort = Convert.ToString(ConfigurationManager.AppSettings["OMSWebAPIPort"]);
        internal string OFSwebAPIPort = Convert.ToString(ConfigurationManager.AppSettings["OFSWebAPIPort"]);

        internal string SMSURL = Convert.ToString(ConfigurationManager.AppSettings["SMSURL"]);
        internal string SMSUserName = Convert.ToString(ConfigurationManager.AppSettings["SMSUserName"]);
        internal string SMSPass = Convert.ToString(ConfigurationManager.AppSettings["SMSPassword"]);
        internal string SMSFrom = Convert.ToString(ConfigurationManager.AppSettings["SMSFrom"]);
        internal UserLoginDataModel UserInfo
        {
            get
            {
                if (Session != null)
                    return (UserLoginDataModel)Session["UserDetails"];
                else
                    return null;
            }
            set
            {
                Session["UserDetails"] = value;
            }
        }
        internal Dictionary<int,string> UserPermissionDetails
        {
            get
            {
                if (Session != null)
                    return (Dictionary<int, string>)Session["UserPermissionDetails"];
                else
                    return null;
            }
            set
            {
                Session["UserPermissionDetails"] = value;
            }
        }
        internal Dictionary<string, PermissionByUser> UserPagePermissionDetails
        {
            get
            {
                if (Session != null)
                    return (Dictionary<string, PermissionByUser>)Session["UserPagePermissionDetails"];
                else
                    return null;
            }
            set
            {
                Session["UserPagePermissionDetails"] = value;
            }
        }


    }
}
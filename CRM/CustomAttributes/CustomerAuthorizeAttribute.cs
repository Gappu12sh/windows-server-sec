using Customer.Data.Application;
using Customer.Model;
using Customer.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CRM.CustomAttributes
{
    public class CustomerAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly int _sessionTimeoutInMinutes;
        public CustomerAuthorizeAttribute()
        {
            _sessionTimeoutInMinutes = int.Parse(ConfigurationManager.AppSettings["TokenExpiryMinutesTime"]);
        }


        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var token = HttpContext.Current.Request.Headers.Get("Authorization")?.Split(' ').Skip(1).FirstOrDefault();
            if (token == null)
            {
                return false;
            }
            LoginSession loginSession = null;
            using (var dbContext = new CustomerApplicationContext())
            {
                var _unitOfWork = new UnitOfWork(dbContext);
                loginSession = _unitOfWork.LoginSessions.FindAll().FirstOrDefault(x => x.LoginToken == token);
                if (loginSession == null)
                {
                    return false;
                }
                // check if current token expired or not
                if (DateTime.Now > loginSession.SessionTimeStamp.AddMinutes(_sessionTimeoutInMinutes))
                {
                    _unitOfWork.LoginSessions.HardDelete(loginSession);
                    _unitOfWork.Save();
                    return false;
                }
                if (!base.IsAuthorized(actionContext))
                {
                    _unitOfWork.LoginSessions.HardDelete(loginSession);
                    _unitOfWork.Save();
                    return false;
                }
                loginSession.SessionTimeStamp = DateTime.Now;
                _unitOfWork.Save();
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var content = new ResponseResults(ErrorCodes.AccessTokenInvalid);
            actionContext.Response.Content = new ObjectContent(content.GetType(), content, new JsonMediaTypeFormatter());
        }

    }
}
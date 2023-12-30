using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Customer.BusinessLogic.Utilities;
using System.Runtime.Remoting.Contexts;
using System.Web.Configuration;
using Customer.Model.Utilities;

namespace Customer.BusinessLogic
{
    public class LoginManager : BaseManager, ILogin
    {
        public LoginManager(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public ResponseResults<string> GetToken(UserLoginModel model)
        {
           var responseToken = GetTokenByUser(model);
            JObject jsonResp = JsonConvert.DeserializeObject<JObject>(responseToken);
            if (jsonResp["error"] != null)
            {
                var response = new ResponseResults<string>(errorCode: Customer.Model.Utilities.ErrorCodes.LoginFailed);
                response.ResponseBody = jsonResp["error_description"].ToString();
                return response;
            }

            return new ResponseResults<string>(jsonResp["access_token"].ToString());
        }

        public string GetTokenByUser(UserLoginModel model)
        {
            var encryptedPwd = new Common().MD5Hash(model.Password);
            model.GrantType = "password";
            var path = "token";
            var requestUri = WebConfigurationManager.AppSettings["baseApi"] +path;
            var postData = $"grant_type={model.GrantType}&username={model.UserName}&password={encryptedPwd}";
            using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
            using (var client = new HttpClient(handler))
            {
                var content = new StringContent(postData, Encoding.UTF8, "application/x-www-form-urlencoded");
                //client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["baseApi"]);
                //client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "true");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
                var response = client.PostAsync(requestUri, content);
                var result = response.Result.Content.ReadAsStringAsync().Result;

                return result;
            }
        }

        //public ResponseResults<UserLoginDataModel> GetUserLoginData(UserTokenModel model)
        //{

        //    var userId = _unitOfWork.LoginSessions.FindAll().FirstOrDefault(x => x.LoginToken == model.Token);
        //    var userEntity = _unitOfWork.UserDetails.FindAll().FirstOrDefault(x => x.UserID == userId.LoginUserId);
        //    var userLoginDataModel = new UserLoginDataModel()
        //    {
        //        AccessToken = model.Token,
        //        Email = userEntity.User_Email,
        //        UserName = userEntity.UserName
        //    };
        //    return new ResponseResults<UserLoginDataModel>(userLoginDataModel);
        //}
        public ResponseResults<UserLoginDataModel> GetUserLoginData(UserLoginModel model)
        {
            var encryptedPwd = new Common().MD5Hash(model.Password);
            //var userId = _unitOfWork.LoginSessions.FindAll().FirstOrDefault(x => x.LoginToken == model.Token);
            //var userEntity = _unitOfWork.UserDetails.FindAll().FirstOrDefault(x => x.UserID == userId.LoginUserId);
            var userEntity = _unitOfWork.UserDetails.FindAll().FirstOrDefault(x => x.User_Email == model.UserName && x.User_Password== encryptedPwd && x.IsActive);
            if(userEntity == null)
            {
                return new ResponseResults<UserLoginDataModel>(ErrorCodes.InValidRequest);
            }
            var userLoginDataModel = new UserLoginDataModel()
            {
                //AccessToken = model.Token,
                Email = userEntity.User_Email,
                UserName = userEntity.UserName,
                UserId=userEntity.UserID,
            };
            return new ResponseResults<UserLoginDataModel>(userLoginDataModel);
        }
        public ResponseResults Logout(string token)
        {
            try
            {
                var session = _unitOfWork.LoginSessions.FindAll().FirstOrDefault(x => x.LoginToken == token);
                if (session != null)
                {
                    _unitOfWork.LoginSessions.SoftDelete(session);
                    _unitOfWork.Save();
                }
            }
            catch 
            {

            }
            return new ResponseResults();
        }
    }
}

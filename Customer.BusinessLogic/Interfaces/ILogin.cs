using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface ILogin
    {
        ResponseResults<string> GetToken(UserLoginModel model);
        //ResponseResults<UserLoginDataModel> GetUserLoginData(UserTokenModel model);
        ResponseResults<UserLoginDataModel> GetUserLoginData(UserLoginModel model);
        ResponseResults Logout(string token);
    }
}

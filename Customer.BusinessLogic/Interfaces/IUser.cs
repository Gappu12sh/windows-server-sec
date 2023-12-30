using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IUser
    {
        ResponseResults CreateUser(UserDetailsModel model);
        ResponseResults UpdateUser(UserDetailsModel model);
        ResponseResults<List<UserDetailsModel>> GetUsers();
        ResponseResults<UserDetailsModel> GetUserById(int Id);
        ResponseResults DeleteUser(int id);
    }
}

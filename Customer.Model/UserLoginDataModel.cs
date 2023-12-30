using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class UserLoginDataModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
    }
}

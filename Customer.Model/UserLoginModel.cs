using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
    }
}

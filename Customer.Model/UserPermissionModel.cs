using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class UserPermissionModel
    {
        public int UserPermissionId { get; set; }
        public int UserModulePermissionId { get; set; }
        public int? ActionId { get; set; }
        public bool IsActive { get; set; }
        public LookupModel Actions { get; set; }
    }
}

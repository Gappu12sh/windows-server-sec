using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class UserModulePermissionModel
    {
        public int UserModulePermissionId { get; set; }
        public int? ModuleId { get; set; }
        public int? UserId { get; set; }
        public bool IsActive { get; set; }
        public bool IsModule { get; set; }
        public ModulesModel module { get; set; }
        public UserDetailsModel user { get; set; }
        public List<UserPermissionModel> userPermissionModels { get; set; } = new List<UserPermissionModel>();
    }
    public class PermissionByUser
    {
        public int UserId { get; set; }
        public bool IsAdd { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdit { get; set; }
        public bool IsView { get; set; }
        public bool IsModule { get; set; }
       public bool IsAddAddress { get; set; }
        public bool IsAddRate { get; set; }
        public bool IsViewParty { get; set; }
    }
}

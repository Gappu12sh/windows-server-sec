using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class UserDetailsModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public int User_Created_By { get; set; }
        public bool IsActive { get; set; }
        public DateTime User_DOE { get; set; }
        public int User_Updated_By { get; set; }
        public DateTime User_DOU { get; set; }
        public string User_Type { get; set; }
        public int? Rep_ID { get; set; }
        public int? User_Dept { get; set; }
        public RepresentativesModel Representatives { get; set; }
    }
    public class CurrentUser
    {
        public int CurrentUserId { get; set; }
    }
    public class UserPermission 
    {
        public List<UserPermissionDetail> UserPermissionDetails { get; set; }
    }
    public class UserPermissionDetail
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }
}

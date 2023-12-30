using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.UserPermissions")]
    public class UserPermission
    {
        [Key]
        public int UserPermissionId { get; set; }
        public int UserModulePermissionId { get; set; }
        public int? ActionId { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("UserModulePermissionId")]
        public UserModulePermission UserModulePermission { get; set; }
        [ForeignKey("ActionId")]
        public Lookup Actions { get; set; }
    }
}

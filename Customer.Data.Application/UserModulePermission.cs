using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.UserModulePermissions")]
    public class UserModulePermission
    {
        [Key]
        public int UserModulePermissionId { get; set; }
        public int? ModuleId { get; set; }
        public int? UserId { get; set; }
        public bool IsModule { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("ModuleId")]
        public Module module { get; set; }
        [ForeignKey("UserId")]
        public UserDetails user { get; set; }
        public ICollection<UserPermission> Permissions { get; set; } = new List<UserPermission>();
    }
}

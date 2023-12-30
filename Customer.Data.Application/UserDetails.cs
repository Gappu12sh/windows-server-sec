using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.UserDetails")]
    public class UserDetails
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public int? User_Created_By { get; set; }
        public bool IsActive { get; set; }
        public DateTime? User_DOE { get; set; }
        public int? User_Updated_By { get; set; }
        public DateTime? User_DOU { get; set; }      
        public string User_Type { get; set; }
        public int? Rep_ID { get; set; }
        public int? User_Dept { get; set; }

        [ForeignKey("Rep_ID")]
        public Representatives Representatives { get; set; }
        [ForeignKey("User_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("User_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
    }
}


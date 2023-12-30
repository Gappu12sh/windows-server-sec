using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.LogInSession")]
    public class LoginSession
    {
        [Key]
        public int LoginSessionId { get; set; }
        [Required]
        public string LoginToken { get; set; }
        public int LoginUserId { get; set; }
        public DateTime SessionTimeStamp { get; set; }
        [ForeignKey("LoginUserId")]
        public virtual UserDetails User { get; set; }
    }
}

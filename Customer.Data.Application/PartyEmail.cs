using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.PartyEmail")]
    public class PartyEmail
    {
        [Key]
        public int PartyEmail_Id { get; set; }
        public int? Party_Id { get; set; }
        public int? Email_Type { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int? PartyEmail_Created_By { get; set; }
        public int? PartyEmail_Updated_By { get; set; }
        public DateTime? PartyEmail_DOE { get; set; }
        public DateTime? PartyEmail_DOU { get; set; }

        [ForeignKey("PartyEmail_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("PartyEmail_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }

        [ForeignKey("Email_Type")]
        public Lookup EmailType { get; set; }
        [ForeignKey("Party_Id")]
        public PartyMaster Party { get; set; }
    }
}

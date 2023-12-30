using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.EmailLists")]
    public class EmailList
    {
        [Key]
        public int EmailListId { get; set; }
        public int? PartyId { get; set; }
        public int? PartyAddressId { get; set; }
        public int? Email_Type { get; set; }
        public string Email { get; set; }
        public int? ContactId { get; set; }
        public bool IsActive { get; set; }
        public int? EmailList_Created_By { get; set; }
        public int? EmailList_Updated_By { get; set; }
        public DateTime? EmailList_DOE { get; set; }
        public DateTime? EmailList_DOU { get; set; }
        [ForeignKey("EmailList_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("EmailList_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("Email_Type")]
        public Lookup EmailType { get; set; }
        //[ForeignKey("PartyId")]
        //public PartyMaster Party { get; set; }
        //[ForeignKey("PartyAddressId")]
        //public PartyAddress PartyAddress { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}

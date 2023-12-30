using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.PhoneLists")]
    public class PhoneList
    {
        [Key]
        public int PhoneListId { get; set; }
        public int? PartyId { get; set; }
        public int? PartyAddressId { get; set; }
        public int? Phone_Type { get; set; }
        public string PhoneNumber { get; set; }
        public int? ContactId { get; set; }
        public bool IsActive { get; set; }
        public int? PhoneList_Created_By { get; set; }
        public int? PhoneList_Updated_By { get; set; }
        public DateTime? PhoneList_DOE { get; set; }
        public DateTime? PhoneList_DOU { get; set; }
        [ForeignKey("PhoneList_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("PhoneList_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("Phone_Type")]
        public Lookup PhoneType { get; set; }
        //[ForeignKey("PartyId")]
        //public PartyMaster Party { get; set; }
        //[ForeignKey("PartyAddressId")]
        //public PartyAddress PartyAddress { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}

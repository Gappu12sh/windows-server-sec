using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.Contacts")]
    public class Contact
    {
        [Key]
        public int Contact_Id { get; set; }
        public int? Party_Id { get; set; }
        public int? PartyAddress_Id { get; set; }
        public string ContactTitle { get; set; }
        public string ContactPersonName { get; set; }
        public string Designation { get; set; }
        public DateTime?DateOfBirth { get; set; }
        public DateTime? DateOfAnniversary { get; set; }
        public bool IsActive { get; set; }
        public int? Contact_Created_By { get; set; }
        public int? Contact_Updated_By { get; set; }
        public DateTime? Contact_DOE { get; set; }
        public DateTime? Contact_DOU { get; set; }

        [ForeignKey("Contact_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("Contact_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("Party_Id")]
        public PartyMaster party { get; set; }
        [ForeignKey("PartyAddress_Id")]
        public PartyAddress address { get; set; }
        public virtual ICollection<EmailList> EmailList { get; set; } = new List<EmailList>();
        public virtual ICollection<PhoneList> PhoneList { get; set; } = new List<PhoneList>();
    }
}

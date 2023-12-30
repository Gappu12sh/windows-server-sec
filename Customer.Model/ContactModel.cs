using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class ContactModel
    {
        public int Contact_Id { get; set; }
        public int Party_Id { get; set; }
        public int? PartyAddress_Id { get; set; }
        public string ContactTitle { get; set; }
        public string ContactPersonName { get; set; }
        public string Designation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfAnniversary { get; set; }
        public bool IsActive { get; set; }
        public int? Contact_Created_By { get; set; }
        public int? Contact_Updated_By { get; set; }
        public DateTime? Contact_DOE { get; set; }
        public DateTime? Contact_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public PartyAddressModel address { get; set; }
        public List<EmailListModel> emailListModels { get; set; } = new List<EmailListModel>();
        public List<PhoneListModel> phoneListModels { get; set; } = new List<PhoneListModel>();
    }
}

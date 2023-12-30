using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class PartyMasterModel
    {
        public int Party_Id { get; set; }
        public string Party_Name { get; set; }
        public string Manufacturer_Type { get; set; }
        public bool IsActive { get; set; }
        public int? Party_Created_By { get; set; }
        public int? Party_Updated_By { get; set; }
        public DateTime? Party_DOE { get; set; }
        public DateTime? Party_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public List<PartyEmailModel> PartyEmails { get; set; } = new List<PartyEmailModel>();
        public List<PartyPhoneModel> PartyPhone { get; set; } = new List<PartyPhoneModel>();
        public List<PartyAddressModel> PartyAddress { get; set; } = new List<PartyAddressModel>();
        //public List<ContactModel> Contact { get; set; }=new List<ContactModel>();
    }
}

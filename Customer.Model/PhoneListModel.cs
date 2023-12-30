using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class PhoneListModel
    {
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
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public LookupModel PhoneType { get; set; }
        public PartyMasterModel Party { get; set; }
        public PartyAddressModel PartyAddress { get; set; }
        [JsonIgnore]
        public ContactModel Contact { get; set; }
    }
}

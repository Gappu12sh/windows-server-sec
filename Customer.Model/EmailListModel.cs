using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class EmailListModel
    {
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
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public LookupModel AddressType { get; set; }
        public PartyMasterModel Party { get; set; }
        public PartyAddressModel PartyAddress { get; set; }
        [JsonIgnore]
        public ContactModel Contact { get; set; }
    }
}

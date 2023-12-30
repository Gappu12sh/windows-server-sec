using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class PartyPhoneModel
    {
        public int PartyPhone_Id { get; set; }
        public int Party_Id { get; set; }
        public int? Phone_Type { get; set; }
        public string Phone_TypeCode { get; set; }
        public string Phone_Number { get; set; }
        public bool IsActive { get; set; }
        public int? PartyPhone_Created_By { get; set; }
        public int? PartyPhone_Updated_By { get; set; }
        public DateTime? PartyPhone_DOE { get; set; }
        public DateTime? PartyPhone_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public LookupModel PhoneType { get; set; }
        [JsonIgnore]
        public PartyMasterModel Party { get; set; }
    }
}

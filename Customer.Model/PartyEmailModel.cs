using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class PartyEmailModel
    {
        public int PartyEmail_Id { get; set; }
        public int? Party_Id { get; set; }
        public int? Email_Type { get; set; }
        public string Email_TypeCode { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int? PartyEmail_Created_By { get; set; }
        public int? PartyEmail_Updated_By { get; set; }
        public DateTime? PartyEmail_DOE { get; set; }
        public DateTime? PartyEmail_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public LookupModel EmailType { get; set; }
        [JsonIgnore]
        public PartyMasterModel Party { get; set; }
    }
}

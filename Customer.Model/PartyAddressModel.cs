using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class PartyAddressModel
    {
        public int PartyAddress_Id { get; set; }
        public int Party_Id { get; set; }
        public string Contact_Email { get; set; }
        public string Contact_Phone { get; set; }
        public int? Address_Type { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string GST { get; set; }
        public bool Supplier_Address { get; set; }
        public string Contact_Remark { get; set; }
        public string Pincode { get; set; }
        public int Representative_Id { get; set; }
        public string Zone { get; set; }
        public string Address_TypeCode { get; set; }
        public bool IsActive { get; set; }
        public int? PartyAddress_Created_By { get; set; }
        public int? PartyAddress_Updated_By { get; set; }
        public DateTime? PartyAddress_DOE { get; set; }
        public DateTime? PartyAddress_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public LookupModel AddressType { get; set; }
        public LookupModel Country { get; set; }
        public LookupModel State { get; set; }
        public LookupModel City { get; set; }
        public RepresentativesModel Representatives { get; set; }
        public string FullAddress { get
            {
                var address = "";
                if (string.IsNullOrEmpty(Address_Line2))
                {
                    address=Address_Line1+","+City?.Description+","+State?.Description+","+Country?.Description;
                }
                else
                {
                    address = Address_Line1 + "," + Address_Line2 + "," + City?.Description + "," + State?.Description + "," + Country?.Description;
                }
                return address;
            }
        }
        public List<ContactModel> Contact { get; set; } = new List<ContactModel>();
    }
    public class AdditionalPartyAddressModel :PartyAddressModel
    {       
        public List<PartyEmailModel> PartyEmails { get; set; } = new List<PartyEmailModel>();
        public List<PartyPhoneModel> PartyPhone { get; set; } = new List<PartyPhoneModel>();
    }
}

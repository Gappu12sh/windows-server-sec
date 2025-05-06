using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.PartAddress")]
    public class PartyAddress
    {
        [Key]
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
        public string GstCertificate { get; set; }
        public bool IsActive { get; set; }
        public int? PartyAddress_Created_By { get; set; }
        public int? PartyAddress_Updated_By { get; set; }
        public DateTime? PartyAddress_DOE { get; set; }
        public DateTime? PartyAddress_DOU { get; set; }

        [ForeignKey("PartyAddress_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("PartyAddress_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("Address_Type")]
        public Lookup AddressType { get; set; }
        [ForeignKey("Party_Id")]
        public PartyMaster Party { get; set; }

        [ForeignKey("CountryId")]
        public Lookup Country { get; set; }
        [ForeignKey("StateId")]
        public Lookup State { get; set; }
        [ForeignKey("CityId")]
        public Lookup City { get; set; }
        [ForeignKey("Representative_Id")]
        public Representatives Representatives { get; set; }
       public virtual ICollection<Contact> Contacts { get; set; }=new List<Contact>();
    }
}

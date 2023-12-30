using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.PartyPhone")]
    public class PartyPhone
    {
        [Key]
        public int PartyPhone_Id { get; set; }
        public int? Party_Id { get; set; }
        public int? Phone_Type { get; set; }
        public string Phone_Number { get; set; }
        public bool IsActive { get; set; }
        public int? PartyPhone_Created_By { get; set; }
        public int? PartyPhone_Updated_By { get; set; }
        public DateTime? PartyPhone_DOE { get; set; }
        public DateTime? PartyPhone_DOU { get; set; }

        [ForeignKey("PartyPhone_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("PartyPhone_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("Phone_Type")]
        public Lookup PhoneType { get; set; }
        [ForeignKey("Party_Id")]
        public PartyMaster Party { get; set; }
    }
}

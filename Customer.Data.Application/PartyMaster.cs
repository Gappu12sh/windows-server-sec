using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.PartyMaster")]
    public class PartyMaster
    {
        [Key]
        public int Party_Id { get; set; }
        public string Party_Name { get; set; }
        public string Manufacturer_Type { get; set; }
        public bool IsActive { get; set; }
        public int? Party_Created_By { get; set; }
        public int? Party_Updated_By { get; set; }
        public DateTime? Party_DOE { get; set; }
        public DateTime? Party_DOU { get; set; }

        [ForeignKey("Party_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("Party_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        public virtual ICollection<PartyEmail> PartyEmails { get; set; } = new List<PartyEmail>();
        public virtual ICollection<PartyPhone> PartyPhone { get; set; } = new List<PartyPhone>();
        public virtual ICollection<PartyAddress> PartyAddress { get; set; } = new List<PartyAddress>();
        //public virtual ICollection<Contact> Contact { get; set; } = new List<Contact>();
    }
}

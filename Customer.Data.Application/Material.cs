using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.Material")]
    public class Material
    {
        [Key]
        public int MaterialId { get; set; } 
        public string PartyName { get; set; }
        public string ProductName { get; set; }
        public string ActualCode { get; set; }
        public decimal TradeDiscount { get; set; }
        public bool IsActive { get; set; }
        public int? PartyId { get; set; }
        public int? ProductId { get; set; }
        public int? Material_Created_By { get; set; }
        public int? Material_Updated_By { get; set; }
        public DateTime? Material_DOE { get; set; }
        public DateTime? Material_DOU { get; set; }

        [ForeignKey("Material_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("Material_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("PartyId")]
        public PartyMaster Party { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public virtual ICollection<MaterialRates> MaterialRates { get; set; }=new List<MaterialRates>();
    }
}

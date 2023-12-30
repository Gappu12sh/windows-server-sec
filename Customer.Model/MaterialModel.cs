using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class MaterialModel
    {
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
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public PartyMasterModel party { get; set; }
        public ProductModel Product { get; set; }
        public virtual ICollection<MaterialRatesModel> MaterialRates { get; set; } = new List<MaterialRatesModel>();
    }
}

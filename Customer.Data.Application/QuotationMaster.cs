using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.QuotationMasters")]
    public class QuotationMaster
    {
        [Key]
        public int QuotationMasterId { get; set; }
        public string QuotationNo { get; set; }
        public string RegisterNo { get; set; }
        public int? PartyId { get; set; }
        public int AddressId { get; set; }
        public string QuotationStatus { get; set; }
        public DateTime? QuotationDate { get; set; }
        public string ShippingAddress { get; set; }
        public string QuotationRepresentative { get; set; }
        public string KindAttTo { get; set; }
        public int FinancialYearId { get; set; }
        public string ShippingAddressRemarks { get; set; }
        public string PartyName { get; set; }
        public int? ContactId { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Terms { get; set; }
        public bool IsActive { get; set; }
        public int? Quotation_Created_By { get; set; }
        public int? Quotation_Updated_By { get; set; }        
        public DateTime? Quotation_DOE { get; set; }
        public DateTime? Quotation_DOU { get; set; }
        [ForeignKey("Quotation_Created_By")]
        public virtual UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("Quotation_Updated_By")]
        public virtual UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("PartyId")]
        public PartyMaster Party { get; set; }
        [ForeignKey("AddressId")]
        public PartyAddress Address { get; set; }
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }

        public virtual ICollection<QuotationDetails> QuotationDetails { get; set; }=new List<QuotationDetails>();
    }
}

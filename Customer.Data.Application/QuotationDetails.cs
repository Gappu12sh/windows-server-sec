using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.QuotationDetails")]
    public class QuotationDetails
    {
        [Key]
        public int QuotationDetailsId { get; set; }
        public int QuotationId { get; set; }
        public string SampleName { get; set; }
        public string ActualName { get; set; }
        public string Group { get; set; }
        public string Quantity { get; set; }
        public int UnitId { get; set; }
        public decimal Rate { get; set; }
        public int? GroupId { get; set; }
        public string ActualNameValue { get; set; }
        public bool IsActive { get; set; }
        public int? QuotationDetails_Created_By { get; set; }
        public int? QuotationDetails_Updated_By { get; set; }
        public DateTime? Quotation_DOE { get; set; }
        public DateTime? Quotation_DOU { get; set; }
        [ForeignKey("QuotationDetails_Created_By")]
        public virtual UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("QuotationDetails_Updated_By")]
        public virtual UserDetails UserDetailsUpdatedBy { get; set; }
        [ForeignKey("QuotationId")]
        public QuotationMaster quotation {  get; set; }
        [ForeignKey("UnitId")]
        public Lookup Unit { get; set; }
    }
}

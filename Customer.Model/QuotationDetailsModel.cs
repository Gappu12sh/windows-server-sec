using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class QuotationDetailsModel
    {
        public int QuotationDetailsId { get; set; }
        public int QuotationId { get; set; }
        public string SampleName { get; set; }
        public string ActualName { get; set; }
        public string Group { get; set; }
        public int? QuantityId { get; set; }
        //public string Quantity { get; set; }
        //public int UnitId { get; set; }
        public decimal Rate { get; set; }
        public int? GroupId { get; set; }
        public string ActualNameValue { get; set; }
        public bool IsActive { get; set; }
        public int? QuotationDetails_Created_By { get; set; }
        public int? QuotationDetails_Updated_By { get; set; }
        public DateTime? Quotation_DOE { get; set; }
        public DateTime? Quotation_DOU { get; set; }

        public int? QuantityMutiple { get; set; }
        public virtual UserDetailsModel UserDetailsCreatedBy { get; set; }
        public virtual UserDetailsModel UserDetailsUpdatedBy { get; set; }

   

        //public LookupModel Unit { get; set; }
        //public string UnitName { get; set; }
        public string QuantityName { get; set; }
        public LookupModel Quantity { get; set; }
    }
}

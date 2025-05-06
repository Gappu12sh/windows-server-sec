using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class QuotationMasterModel
    {
        public int QuotationMasterId { get; set; }
        public string QuotationNo { get; set; }
        public string RegisterNo { get; set; }
        public int PartyId { get; set; }
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
        //public string StandardQuoteDate { get; set; }
        public virtual UserDetailsModel UserDetailsCreatedBy { get; set; }
        public virtual UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public PartyMasterModel Party { get; set; }
        public PartyAddressModel Address { get; set; }
        public ContactModel Contact { get; set; }
        public List<QuotationDetailsModel> QuotationDetails { get; set;}=new List<QuotationDetailsModel>();
    }
    
    public class SampleProductDetails
    {
        public List<SampleNameModel> SampleNames { get; set; }=new List<SampleNameModel>();
        //public List<ProductModel> Products { get; set; }=new List<ProductModel>();
    }
    public class SampleNameModel
    {
        public string SampleName { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal LastSampleRate { get; set; }
    }
}

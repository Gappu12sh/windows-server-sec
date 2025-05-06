using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class PurchaseOrderModel
    {
        public int PurchaseOrderId { get; set; }
        public string SNo { get; set; }
        public DateTime? Date { get; set; }
        public string PONo { get; set; }
        public DateTime? PODate { get; set; }
        public string GSTNo { get; set; }
        public string DispatchCity { get; set; }
        public string DispatchVia { get; set; }
        public string ExecutiveName { get; set; }
        public string Remark { get; set; }
        public string MaterialDelAddress { get; set; }
        public string PackedBy { get; set; }
        public DateTime? PackedDate { get; set; }
        public string Label { get; set; }
        public string Bottle { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsActive { get; set; }
        public int? PartyId { get; set; }
        public int? PartyAddressId { get; set; }
        public int? SampleNumberId { get; set; }
        public string PartyName { get; set; }
        public string PartyAddress { get; set; }
        public string SampleNumber { get; set; }
        public string CourierChg { get; set; }
        public string PaymentTerms { get; set; }
        public int? PO_Created_By { get; set; }
        public int? PO_Updated_By { get; set; }
        public DateTime? PO_DOE { get; set; }
        public DateTime? PO_DOU { get; set; }
        public UserDetailsModel UserDetailsCreatedBy { get; set; }
        public UserDetailsModel UserDetailsUpdatedBy { get; set; }
        public List<PurchaseOrderDetailsModel> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetailsModel>();
    }    
}

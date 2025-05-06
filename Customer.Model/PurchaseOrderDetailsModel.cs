using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model
{
    public class PurchaseOrderDetailsModel
    {
        public int PurchaseOrderDetailsId { get; set; }
        public int PurchaseOrderId { get; set; }
        public string ItemName { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Packing { get; set; }
        public string TDPer { get; set; }
        public string BatchNo { get; set; }
        public string InternalCode { get; set; }
        public DateTime? MfgDate { get; set; }
        public bool IsActive { get; set; }
        public int? POD_Created_By { get; set; }
        public int? POD_Updated_By { get; set; }
        public DateTime? PO_DOE { get; set; }
        public DateTime? PO_DOU { get; set; }
    }
}

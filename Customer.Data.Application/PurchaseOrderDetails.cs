using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Data.Application
{
    [Table("app.PurchaseOrderDetails")]
    public class PurchaseOrderDetails
    {
        [Key]
        public int PurchaseOrderDetailsId { get; set; }
        public int PurchaseOrderId { get; set; }
        public string ItemName { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public string Packing { get; set; }
        public string TDPer {  get; set; }
        public string BatchNo { get; set; }
        public string InternalCode { get; set; }
        public DateTime? MfgDate { get; set; }
        public bool IsActive { get; set; }
        public int? POD_Created_By { get; set; }
        public int? POD_Updated_By { get; set; }
        public DateTime? POD_DOE { get; set; }
        public DateTime? POD_DOU { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }
        [ForeignKey("POD_Created_By")]
        public UserDetails UserDetailsCreatedBy { get; set; }
        [ForeignKey("POD_Updated_By")]
        public UserDetails UserDetailsUpdatedBy { get; set; }
    }
}

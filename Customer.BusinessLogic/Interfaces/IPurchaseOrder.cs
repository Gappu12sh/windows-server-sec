using Customer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic.Interfaces
{
    public interface IPurchaseOrder
    {
        ResponseResults CreatePurchaseOrder(PurchaseOrderModel model);
        ResponseResults UpdatePurchaseOrder(PurchaseOrderModel model);
        ResponseResults<List<PurchaseOrderModel>> GetPurchaseOrder();
        ResponseResults<PurchaseOrderModel> GetPurchaseOrderById(int Id);
        ResponseResults DeletePurchaseOrder(int id);
        int GetLastPurchaseOrderId();
         bool CheckPONumberExists(string poNumber);
    }
}

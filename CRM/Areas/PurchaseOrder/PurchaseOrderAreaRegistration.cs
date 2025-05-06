using System.Web.Mvc;

namespace CRM.Areas.PurchaseOrder
{
    public class PurchaseOrderAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PurchaseOrder";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PurchaseOrder_default",
                "PurchaseOrder/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
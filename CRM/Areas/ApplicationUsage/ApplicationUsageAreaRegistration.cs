using System.Web.Mvc;

namespace CRM.Areas.ApplicationUsage
{
    public class ApplicationUsageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ApplicationUsage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ApplicationUsage_default",
                "ApplicationUsage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
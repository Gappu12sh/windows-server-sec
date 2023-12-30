using System.Web.Mvc;

namespace CRM.Areas.PartyAddress
{
    public class PartyAddressAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PartyAddress";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PartyAddress_default",
                "PartyAddress/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System.Web.Mvc;

namespace CRM.Areas.Material
{
    public class MaterialAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Material";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Material_default",
                "Material/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
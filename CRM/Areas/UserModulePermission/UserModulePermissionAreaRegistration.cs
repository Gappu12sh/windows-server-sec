using System.Web.Mvc;

namespace CRM.Areas.UserModulePermission
{
    public class UserModulePermissionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserModulePermission";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserModulePermission_default",
                "UserModulePermission/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
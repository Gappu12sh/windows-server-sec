using System.Web.Mvc;

namespace CRM.Areas.ChangePassword
{
    public class ChangePasswordAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ChangePassword";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ChangePassword_default",
                "ChangePassword/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
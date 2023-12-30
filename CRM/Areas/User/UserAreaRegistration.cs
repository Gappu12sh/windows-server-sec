using System.Web.Mvc;

namespace CRM.Areas.User
{
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
          "Default1",
          "",
          defaults: new { area = "User", controller = "Account", action = "Login", id = UrlParameter.Optional },
           namespaces: new[] { "CRM.Areas.User.Controllers" }
          );

            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "Login", id = UrlParameter.Optional },
              namespaces: new[] { "CRM.Areas.User.Controllers" }
            );
        }
    }
}

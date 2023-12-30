using CRM.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using static CRM.Provider.AuthorizationProvider;

[assembly: OwinStartup(typeof(CRM.Startup))]

namespace CRM
{
    public class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString(ConfigurationManager.AppSettings["EndPointTokenPath"]),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["SignInExpiryHoursToken"])),
                Provider = new AuthorizationProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(),
            };
            
            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

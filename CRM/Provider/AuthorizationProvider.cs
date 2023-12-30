using Customer.Data.Application;
using Customer.Model;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CRM.Provider
{
    public class AuthorizationProvider : OAuthAuthorizationServerProvider
    {
        CustomerApplicationContext _context = new CustomerApplicationContext();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });            
            UserDetails user = _context.UserDetails.SingleOrDefault(x => x.User_Email == context.UserName && x.User_Password == context.Password);
            identity.AddClaim(new Claim("UserID", user.UserID.ToString()));
            var props = new AuthenticationProperties(new Dictionary<string, string> {
                    {
                        "User_Email",
                        user.User_Email
                    },
                    {
                        "UserId",
                        user.UserID.ToString()
                    }
                });
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {

            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var AccessToken = context.AccessToken;
            var userId = Convert.ToInt32(context.Identity.Claims.ToList()[0].Value);
            var userSession = new LoginSession()
            {
                LoginToken = context.AccessToken,
                SessionTimeStamp = DateTime.Now,
                LoginUserId = userId,
            };

            _context.LoginSession.Add(userSession);
            _context.SaveChanges();
            return base.TokenEndpointResponse(context);
        }
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult<object>(null);
        }
        public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
        {
            private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

            public void Create(AuthenticationTokenCreateContext context)
            {
                var guid = Guid.NewGuid().ToString();
                _refreshTokens.TryAdd(guid, context.Ticket);
                context.SetToken(guid);
            }

            public async Task CreateAsync(AuthenticationTokenCreateContext context)
            {
                var guid = Guid.NewGuid().ToString();
                _refreshTokens.TryAdd(guid, context.Ticket);
                context.SetToken(guid);
            }

            public void Receive(AuthenticationTokenReceiveContext context)
            {
                throw new NotImplementedException();
            }

            public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
            {
                AuthenticationTicket ticket;
                if (_refreshTokens.TryRemove(context.Token, out ticket))
                {
                    context.SetTicket(ticket);
                }
            }
        }
    }
}
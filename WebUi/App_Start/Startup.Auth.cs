using System;
using Luval.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using WebUi.App_Start;

[assembly: OwinStartup(typeof(Startup))]
namespace WebUi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ISecurityManager>(() => new UserStoreProvider());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                ExpireTimeSpan = TimeSpan.FromMinutes(30)
            });

            // Enable the External Sign In Cookie.
            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);

            // Enable Social Authentication
            app.UseGoogleAuthentication(GetGoogleOptions());
            app.UseFacebookAuthentication(GetFacebookOptions());
        }

        private static GoogleOAuth2AuthenticationOptions GetGoogleOptions()
        {
            var reader = new KeyReader();
            var keys = reader.GetKey("google");
            var options = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = keys.Public,
                ClientSecret = keys.Private
            };
            return options;
        }

        private static FacebookAuthenticationOptions GetFacebookOptions()
        {
            var reader = new KeyReader();
            var keys = reader.GetKey("facebook");
            var options = new FacebookAuthenticationOptions()
            {
                AppId = keys.Public,
                AppSecret = keys.Private
            };
            options.Scope.Add("email");
            return options;
        }
    }
}
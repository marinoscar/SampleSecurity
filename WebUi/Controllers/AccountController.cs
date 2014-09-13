using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Luval.Security;
using Luval.Security.Model;
using WebUi.App_Start;

namespace WebUi.Controllers
{
    public class AccountController : Controller
    {

        #region Variable Declaration

        private ISecurityManager _storeProvider;
        private IOwinContext _owinContext;

        #endregion

        #region Property Implementation

        public ISecurityManager StoreProvider
        {
            get { return _storeProvider ?? (_storeProvider = OwinContext.Get<ISecurityManager>()); }
        }

        public IOwinContext OwinContext
        {
            get { return _owinContext ?? (_owinContext = HttpContext.GetOwinContext()); }
        }

        public IAuthenticationManager AuthenticationManager
        {
            get { return OwinContext.Authentication; }
        }

        #endregion

        #region Password Authentication Actions

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginInfo, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginInfo);
            }
            var result = StoreProvider.SignInPassword(loginInfo.UserName, loginInfo.Password, loginInfo.RememberMe, OwinContext);
            switch (result)
            {
                case SignInStatus.Failure:
                    ModelState.AddModelError("", "Invalid email or password");
                    break;
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("", "User is locked");
                    break;
            }
            return RedirectToLocal(returnUrl);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        } 

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        #endregion

        #region External Login Actions

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider,
                                       Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        [AllowAnonymous, HttpGet]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null || !loginInfo.ExternalIdentity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }

            AuthenticationManager.SignIn(loginInfo.ExternalIdentity);
            return RedirectToLocal(returnUrl);
        }

        #endregion

        #region Account Management Actions

        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var password = new PasswordProvider().CreatePassword(model.Password);
            var user = new User()
                {
                    UserName = model.UserName,
                    LoweredUserName = model.UserName.ToLowerInvariant(),
                    PrimaryEmail = model.UserName,
                    Name = model.Name,
                    LastName = model.LastName,
                    PasswordHash = password.PasswordHash,
                    PasswordSalt = password.Salt
                };

            var task = StoreProvider.CreateAsync(user);
            task.RunSynchronously();
            task.Wait();

            return RedirectToAction("Login");
        } 

        #endregion

        #region Helper Methods

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}

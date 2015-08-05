using LonghornMusic.Infrastructure;
using LonghornMusic.Models;
using LonghornMusic.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;

namespace LonghornMusic.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            
                ViewBag.returnUrl = returnUrl;
                return View();
            
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel details, string returnUrl)
        {
            //if user is already logged in, they are trying to do something that they aren't authorized for, so redirect
            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                return View("Error", new string[] { "Access Denied" });
            }
            
            
            //set default return url to homepage
            if (returnUrl == "" || returnUrl == null)
            {
                returnUrl = "/Home/Index";
            }
            if (ModelState.IsValid)
            {
                AppUser user = UserManager.Find(details.Email, details.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else 
                {
                    ClaimsIdentity ident = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(returnUrl);
                }
            }
            return View(details);
        }


        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthManager
        {
            get 
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}
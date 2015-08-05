using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LonghornMusic.DAL;
using LonghornMusic.ViewModels;
using LonghornMusic.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LonghornMusic.Models;

namespace LonghornMusic.Controllers
{
    public class HomeController : Controller
    {
                
        // GET: Home

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Songs");
            //return View(GetData("Index"));
        }

        [Authorize(Roles = "Users")]
        public ActionResult OtherAction()
        {
            return View("Index", GetData("OtherAction"));
        }

                
        private Dictionary<string, object> GetData(string actionName) 
        {
            //Create the dictionary
            Dictionary<string, object> dict = new Dictionary<string, object>();

            //Add stuff to it
            dict.Add("Action", actionName);
            dict.Add("User", HttpContext.User.Identity.Name);
            dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
            dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
            dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
            //dict.Add("Email", CurrentUser.Email);
            //dict.Add("Street Address", CurrentUser.StreetAddress);
            //dict.Add("Password", CurrentUser.PasswordHash);

            //return the dictionary
            return dict;
        }


        //methods for managing users
        private AppUser CurrentUser
        {
            get 
            {
                return UserManager.FindByName(HttpContext.User.Identity.Name);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace SaleStatistic.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var users = UserManager.Users;
            return View(users);
        }

        private ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return Redirect("/Account/Register");
        }
    }
}
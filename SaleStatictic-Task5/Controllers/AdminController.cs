using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SaleStatictic_Task5.Models;

namespace SaleStatictic_Task5.Controllers
{
    public class AdminController : Controller
    {
      private ApplicationUserManager UserManager => HttpContext.GetOwinContext()
           .GetUserManager<ApplicationUserManager>();
        // GET: Admin
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }
       
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = UserManager.Users.First(x => x.Id == id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        // GET: ManagerViewModels/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.Users.First(x => x.Id == id);
            
            if (user == null)
            {
                return HttpNotFound();
            }
            var role = UserManager.GetRoles(user.Id).ElementAt(0);
            ViewBag.Role = role;
            return View(user);
        }
    }
}
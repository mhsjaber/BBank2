using BloodBank.Authorization;
using BloodBank.Models;
using BloodBank.Models.EntityDiagram;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace BloodBank.Controllers
{
    [CustomAuthorizeUser(UserType = UserType.Admin)]
    public class AdminController : Controller
    {
        private DonorDBContext context = new DonorDBContext();
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = context.Admin.ToList()
                    .Where(x => x.Username.ToLower() == model.Username.ToLower() &&
                        x.Password == model.Password)
                    .FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session["UserType"] = UserType.Admin;
                    HttpContext.Session["UserID"] = user.ID.ToString();
                    HttpContext.Session["Username"] = user.Username;
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View(context.Admin.Find(Guid.Parse(HttpContext.Session["UserID"].ToString())));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "ID,Username,Password,RePassword,OldPassword")]Admin admin)
        {
            var oldAdmin = context.Admin.Find(admin.ID);
            if (admin.Password == admin.RePassword && oldAdmin.Password == admin.OldPassword && ModelState.IsValid)
            {
                context.Set<Admin>().AddOrUpdate(admin);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(context.Admin.Find(Guid.Parse(HttpContext.Session["UserID"].ToString())));
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
using BloodBank.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodBank.Controllers
{
    [CustomAuthorizeUser(UserType = UserType.Admin)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            HttpContext.Session["UserType"] = UserType.Admin.ToString();
            string x = (string)HttpContext.Session["UserType"];
            return View();
        }
    }
}
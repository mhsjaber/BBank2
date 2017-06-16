using BloodBank.Authorization;
using BloodBank.Models.EntityDiagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodBank.Controllers
{
    public class HomeController : Controller
    {
        public DonorDBContext context = new DonorDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var model = new ContactMessage();
            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                model.ID = Guid.NewGuid();
                model.CreatedOn = DateTime.Now;
                context.ContactMessage.Add(model);
                context.SaveChanges();
                return RedirectToAction("/Contact");
            }
            return View(model);
        }
    }
}
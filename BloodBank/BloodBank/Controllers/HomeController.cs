using BloodBank.Authorization;
using BloodBank.Models;
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
        private DonorDBContext context = new DonorDBContext();
        public ActionResult Index()
        {
            var model = new HomeModel();
            model.About = context.About.ToList().FirstOrDefault();
            model.Events = context.Event.ToList().Where(x => x.EventDate >= DateTime.Now).OrderBy(x => x.EventDate).ToList().Take(3).ToList();
            model.Donors = context.Donor.ToList().Where(x => x.Status == AccountStatus.Active).OrderBy(x => x.CreatedOn).ToList().Take(3).ToList();
            return View(model);
        }

        public ActionResult About()
        {
            var model = context.About.ToList().FirstOrDefault();
            return View(model);
        }

        public ActionResult Contact()
        {
            var model = new ContactMessage();
            return View(model);
        }

        public ActionResult Events(string type)
        {
            if (type == null || type.ToLower() != "archived")
            {
                var model = context.Event.ToList().Where(x => x.EventDate >= DateTime.Now).OrderBy(x => x.EventDate).ToList();
                return View(model);
            }
            else
            {
                var model = context.Event.ToList().Where(x => x.EventDate < DateTime.Now).OrderByDescending(x => x.EventDate).ToList();
                return View(model);
            }
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
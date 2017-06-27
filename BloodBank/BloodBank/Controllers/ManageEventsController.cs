using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodBank.Models.EntityDiagram;
using BloodBank.Authorization;

namespace BloodBank.Controllers
{
    [CustomAuthorizeUser(UserType = UserType.Admin)]
    public class ManageEventsController : Controller
    {
        private DonorDBContext db = new DonorDBContext();

        public ActionResult Index()
        {
            return View(db.Event.ToList());
        }

        public ActionResult Archived()
        {
            return View(db.Event.ToList().Where(x => x.EventDate <= DateTime.Now).ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Description,Location,Objective,EventDate")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.ID = Guid.NewGuid();
                db.Event.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Event.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,Location,Objective,EventDate")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            Event @event = db.Event.Find(id);
            db.Event.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    public class ManageMessageController : Controller
    {
        private DonorDBContext db = new DonorDBContext();

        public ActionResult Index()
        {
            return View(db.ContactMessage.ToList().OrderByDescending(x=> x.CreatedOn).ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactMessage contactMessage = db.ContactMessage.Find(id);
            if (contactMessage == null)
            {
                return HttpNotFound();
            }
            return View(contactMessage);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContactMessage contactMessage = db.ContactMessage.Find(id);
            db.ContactMessage.Remove(contactMessage);
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

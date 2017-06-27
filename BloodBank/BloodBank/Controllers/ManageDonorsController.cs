using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodBank.Models.EntityDiagram;

namespace BloodBank.Controllers
{
    public class ManageDonorsController : Controller
    {
        private DonorDBContext db = new DonorDBContext();

        public ActionResult Index()
        {
            return View(db.Donor.ToList());
        }

        public ActionResult Pending()
        {
            return View(db.Donor.ToList().Where(x => x.Status == AccountStatus.Pending).ToList());
        }

        public ActionResult Blocked()
        {
            return View(db.Donor.ToList().Where(x => x.Status == AccountStatus.Blocked).ToList());
        }

        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donor.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,Mobile,Address,Area,District,Email,Password,Username,CreatedOn,DateOfBirth,BloodGroup,Status,DontationStatus")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                donor.ID = Guid.NewGuid();
                db.Donor.Add(donor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donor);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donor.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,Mobile,Address,Area,District,Email,Password,Username,CreatedOn,DateOfBirth,BloodGroup,Status,DontationStatus")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donor);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donor donor = db.Donor.Find(id);
            if (donor == null)
            {
                return HttpNotFound();
            }
            return View(donor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Donor donor = db.Donor.Find(id);
            db.Donor.Remove(donor);
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

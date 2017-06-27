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
    public class ManageDonorsController : Controller
    {
        private DonorDBContext db = new DonorDBContext();

        public ActionResult Index()
        {
            return View(db.Donor.ToList().OrderByDescending(x => x.CreatedOn).ToList());
        }

        public ActionResult Pending()
        {
            return View(db.Donor.ToList().Where(x => x.Status == AccountStatus.Pending).ToList().OrderByDescending(x => x.CreatedOn).ToList());
        }

        public ActionResult Blocked()
        {
            return View(db.Donor.ToList().Where(x => x.Status == AccountStatus.Blocked).ToList().OrderByDescending(x => x.CreatedOn).ToList());
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
        public ActionResult Edit([Bind(Include = "ID,FullName,Mobile,Address,Area,District,Email,Password,Username,CreatedOn,DateOfBirth,BloodGroup,Status,DonationStatus")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donor);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
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

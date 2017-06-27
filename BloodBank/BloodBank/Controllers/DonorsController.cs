using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BloodBank.Models.EntityDiagram;
using BloodBank.Models;
using BloodBank.Authorization;
using System.Data.Entity.Validation;

namespace BloodBank.Controllers
{
    [CustomAuthorizeUser(UserType = UserType.Donor)]
    public class DonorsController : Controller
    {
        private DonorDBContext context = new DonorDBContext();
        [AllowAnonymous]
        public ActionResult Index(string bloodGroup = "", string area = "", string district = "")
        {
            var model = context.Donor
                .ToList()
                .Where(x => x.Area.ToLower().Contains(area.ToLower()) &&
                    x.District.ToLower().Contains(district.ToLower()) &&
                    x.Status == AccountStatus.Active)
                .ToList()
                .OrderBy(x => x.BloodGroup)
                .ThenBy(x => x.Area)
                .ToList();

            if (!string.IsNullOrWhiteSpace(bloodGroup))
                model = model.Where(x => x.BloodGroup.ToLower() == bloodGroup.ToLower()).ToList();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new Donor());
        }

        [AllowAnonymous, HttpPost]
        public ActionResult Register(Donor model)
        {
            if (model.FullName != null && model.Mobile != null && model.Address != null && model.Area != null &&
                model.District != null && model.Email != null && model.Password != null && model.Username != null && 
                model.DateOfBirth != null && model.BloodGroup != null && model.DonationStatus != null)
            {
                model.Status = AccountStatus.Pending;
                model.CreatedOn = DateTime.Now;
                model.ID = Guid.NewGuid();

                var user = context.Donor.ToList().Where(x => x.Username.ToLower() == model.Username.ToLower()).FirstOrDefault();
                if (user == null)
                {
                    context.Donor.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(model);
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
                var user = context.Donor.ToList()
                    .Where(x=> x.Username.ToLower() == model.Username.ToLower() && 
                        x.Password == model.Password)
                    .FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session["UserType"] = UserType.Donor;
                    HttpContext.Session["UserID"] = user.ID.ToString();
                    HttpContext.Session["Username"] = user.Username;
                    return RedirectToAction("ProfileView");
                }
            }
            return View(model);
        }

        public ActionResult ProfileView()
        {
            var user = context.Donor.Find(Guid.Parse(HttpContext.Session["UserID"].ToString()));
            return View(user);
        }

        [HttpPost]
        public ActionResult ProfileView(Donor model)
        {
            if (model.FullName != null && model.Mobile != null && model.Address != null && model.Area != null &&
                model.District != null && model.Email != null &&  model.DateOfBirth != null && 
                model.BloodGroup != null && model.DonationStatus != null)
            {
                var user = context.Donor.ToList().Where(x=> x.ID == Guid.Parse(HttpContext.Session["UserID"].ToString())).FirstOrDefault();
                user.Address = model.Address;
                user.Area = model.Area;
                user.BloodGroup = model.BloodGroup;
                user.DateOfBirth = model.DateOfBirth;
                user.District = model.District;
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.Mobile = model.Mobile;
                user.DonationStatus = model.DonationStatus;

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ProfileView");
            }
           return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

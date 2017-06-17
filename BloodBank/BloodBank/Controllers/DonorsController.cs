using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BloodBank.Models.EntityDiagram;
using BloodBank.Models;
using BloodBank.Authorization;

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
                .Where(x => x.BloodGroup.ToLower().Contains(bloodGroup.ToLower()) &&
                    x.Area.ToLower().Contains(area.ToLower()) &&
                    x.District.ToLower().Contains(district.ToLower()) &&
                    x.Status == AccountStatus.Active)
                .ToList()
                .OrderBy(x => x.District)
                .ThenBy(x => x.Area)
                .ToList();
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
            if (ModelState.IsValid && model.Password == model.RePassword)
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
        public ActionResult ProfileView([Bind(Include = "FullName,Mobile,Address,Area,District,Email,Username,DateOfBirth,BloodGroup")] Donor donor)
        {
            if (ModelState.IsValid)
            {
                var user = context.Donor.ToList().Where(x=> x.ID == Guid.Parse(HttpContext.Session["UserID"].ToString())).FirstOrDefault();
                user.Address = donor.Address;
                user.Area = donor.Area;
                user.BloodGroup = donor.BloodGroup;
                user.DateOfBirth = donor.DateOfBirth;
                user.District = donor.District;
                user.Email = donor.Email;
                user.FullName = donor.FullName;
                user.Mobile = donor.Mobile;

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ProfileView");
            }
            return View(donor);
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

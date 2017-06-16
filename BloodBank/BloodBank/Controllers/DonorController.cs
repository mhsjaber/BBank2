using BloodBank.Models.EntityDiagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodBank.Controllers
{
    public class DonorController : Controller
    {
        private DonorDBContext context = new DonorDBContext();
        public ActionResult Index(string bloodGroup = "", string area = "", string district = "")
        {
            var model = context.Donor
                .ToList()
                .Where(x => x.BloodGroup.ToLower().Contains(bloodGroup.ToLower()) &&
                    x.Area.ToLower().Contains(area.ToLower()) &&
                    x.District.ToLower().Contains(district.ToLower()) &&
                    x.Status == AccountStatus.Active)
                .ToList()
                .OrderBy(x=> x.District)
                .ThenBy(x=> x.Area)
                .ToList();
            return View(model);
        }
    }
}
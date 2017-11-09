using DagensTV.Authority;
using DagensTV.Data;
using DagensTV.Models;
using DagensTV.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Controllers
{
    [AuthorizeRoles("Redaktör")]
    public class AdminController : Controller
    {
        DagensTVEntities db = new DagensTVEntities();
        DbOperations dbo = new DbOperations();

        // GET: Admin
        
        [HttpGet]        
        public ActionResult Index()
        {
            #region Set PopVM
            PopVM pop = new PopVM();
            pop.Channels.AddRange(dbo.GetAllChannels());

            IEnumerable<SelectListItem> popular = db.PopularContent
              .Select(c => new SelectListItem
              {
                  Value = c.Id.ToString(),
                  Text = c.Schedule.Show.Name,
              });
            ViewBag.Pops = popular;

            IEnumerable<SelectListItem> schedules = db.Schedule
             .Select(c => new SelectListItem
             {
                 Value = c.Id.ToString(),
                 Text = c.Show.Name
             });
            ViewBag.Schedule = schedules;
            #endregion

            return View(pop);
        }

        [HttpPost]
        public ActionResult GetForm(PopVM model)
        {
            if (ModelState.IsValid)
            {
                #region Update

                dbo.UpdatePopularContent(model);

                #endregion
            }
            else
            {
                ModelState.AddModelError(string.Empty, "En bild måste anges!");
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}
using DagensTV.Models;
using DagensTV.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Controllers
{
    
    public class AdminController : Controller
    {
        DagensTVEntities db = new DagensTVEntities();

        // GET: Admin
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var popularContent = db.PopularContent.OrderBy(s => s.Id).Select(x => new PopVM
            {
                Id = x.Id,
                ImgUrl = x.ImgUrl,
                Name = x.Schedule.Show.Name,
                Icon = x.Icon,
                ScheduleId = x.ScheduleId,
                Schedules = db.Schedule.Where(s => s.Id == x.ScheduleId).Select(schedule => new ScheduleVM
                {
                    Id = schedule.Id,
                    StartTime = schedule.StartTime,
                    ChannelId = schedule.ChannelId,
                    ChannelName = schedule.Channel.Name,
                    ShowName = schedule.Show.Name
                }).ToList()
            });

            return View(popularContent);
        }

        [HttpPost]
        public ActionResult GetForm(PopVM model)
        {
           


            return View();
        }
    }
}
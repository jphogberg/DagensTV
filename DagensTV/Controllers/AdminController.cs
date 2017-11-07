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
            /*Byggt om PopVM så att den bara består av EN modell med två listor i.
             EN modell gör att jag kan nå propertiesarna genom @html.helpers för textboxarna.
             Listorna för att kunna fylla comboboxarna.
             Need to fill the lists correctly though.......*/

            PopVM pop = new PopVM();

            //Behöver göra om hämtningen från db = tilldela pop.popular med <popularContent>
            //och pop.schedules med <scheduleVM>

            //pop.Schedule = db.Schedule.Select(x => x.F);
            pop.Popular = db.PopularContent.OrderBy(s => s.Id).Select(x => new PopVM
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
                    ChannelName = schedule.Channel.Name,
                    ShowName = schedule.Show.Name,
                    ChannelId = schedule.ChannelId
                }).ToList()
            });

            pop.Channels = db.Channel.ToList();
            //foreach(var item in popularContent)
            //{
            //    pop.Popular.AddRange(item.Popular);
            //}


            return View(pop);
        }

        [HttpPost]
        public ActionResult GetForm(PopVM model)
        {
            //får en PopVM med två listor
            //uppdatera db.PopularContent
            //var används PopVM.Name????
            //ladda upp de nya nyhetspuffarna på startsidan



            return View();
        }
    }
}
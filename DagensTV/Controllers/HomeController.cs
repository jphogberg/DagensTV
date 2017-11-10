using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DagensTV.Models;
using DagensTV.Models.ViewModels;
using Newtonsoft.Json;
using System.Net;
using DagensTV.Data;

namespace DagensTV.Controllers
{
    public class HomeController : Controller
    {
        DagensTVEntities db = new DagensTVEntities();
        DbOperations dbo = new DbOperations();

        #region Main               
        public ActionResult Index(string date)
        {
            if (date == null)
            {
                var today = DateTime.Now;
                date = today.ToShortDateString();
            }          
            
            if (date == DateTime.Now.ToShortDateString())
            {
                var dateText = "Dagens tv-tablå";
                ViewBag.Date = dateText;
            }
            else if (date == DateTime.Now.AddDays(1).ToShortDateString())
            {
                var dateText = "Tv-tablå imorgon";
                ViewBag.Date = dateText;
            }
            else
            {
                var dateParse = DateTime.Parse(date).DayOfWeek;
                var day = new System.Globalization.CultureInfo("sv-SE").DateTimeFormat.GetDayName(dateParse);
                var dateText = "Tv-tablå för " + day;
                ViewBag.Date = dateText;
            }

            if (Person.activeUser.Id != 0)
            {
                return View(dbo.GetSchedule(date, Person.activeUser.Id));
            }

            return View(dbo.GetSchedule(date));
        }

        [ActionName("FilterSchedule")]
        public ActionResult Index(string Filter, string date)
        {
            if (date == null)
            {
                var today = DateTime.Now;
                date = today.ToShortDateString();
            }                       

            if (date == DateTime.Now.ToShortDateString())
            {
                var dateText = Filter + " på tv idag";
                ViewBag.Date = dateText;
            }
            else if (date == DateTime.Now.AddDays(1).ToShortDateString())
            {
                var dateText = Filter + " på tv imorgon";
                ViewBag.Date = dateText;
            }
            else
            {
                var dateParse = DateTime.Parse(date).DayOfWeek;
                var day = new System.Globalization.CultureInfo("sv-SE").DateTimeFormat.GetDayName(dateParse);
                var dateText = Filter + " på tv på " + day;
                ViewBag.Date = dateText;
            }           
            return PartialView("_TvSchedules", dbo.FilterScheduleByCategory(Filter, date));
        }

        public ActionResult ShowInfo(int Id)
        {
            var scheduleList = dbo.ShowInfo(Id);                       

            ViewBag.ScheduleList = scheduleList;

            return PartialView("_Overlay");
        }       
        #endregion

        #region Popular Content
        public ActionResult PopIndex()
        {
            var popList = db.PopularContent.OrderBy(s => s.Id).Select(x => new PopVM
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

            return PartialView("_PopularContent", popList);
        }
        #endregion

    }
}
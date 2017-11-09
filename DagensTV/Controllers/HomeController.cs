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

            ViewBag.Date = date;

            if (Person.activeUser.Id != 0)
            {
                return View(dbo.GetSchedule(date, Person.activeUser.Id));
            }

            //dbo.GetShowsFromJson(date);
            //dbo.GetScheduleFromJson(date);

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

            return PartialView("_TvSchedules", dbo.FilterScheduleByCategory(Filter));
        }

        public ActionResult ShowInfo(int Id)
        {
            List<ScheduleVM> scheduleList = db.Schedule.Where(x => x.Id == Id).Select(x => new ScheduleVM
            {
                StartTime = x.StartTime,
                EndTime = (DateTime)DbFunctions.AddMinutes(x.StartTime, x.Duration),
                //EndTime = DbFunctions.AddMinutes(x.StartTime, x.Duration),
                ChannelName = x.Channel.Name,
                ShowName = x.Show.Name,
                Resume = x.Resume,
                CategoryName = x.Show.Category.Name,
                CategoryTag = x.Show.Category.Tag,
                MovieGenre = x.Show.MovieGenre,
                ImdbRating = x.Show.ImdbRating,
                StarImage = x.Show.RatingIcon
            }).ToList();

            ViewBag.ScheduleList = scheduleList;

            return PartialView("_Overlay");
        }

        public ActionResult ActiveShow()
        {

            return View();
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
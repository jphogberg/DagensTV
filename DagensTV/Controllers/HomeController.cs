using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DagensTV.Models;
using DagensTV.Models.ViewModels;

namespace DagensTV.Controllers
{
    public class HomeController : Controller
    {
        DagensTVEntities db = new DagensTVEntities();

        #region Main
        public ActionResult Index()
        {            
            var channelList = db.Channel.Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && DbFunctions.AddMinutes(s.StartTime, s.Duration) > DateTime.Now).Select(schedule => new ScheduleVM
                {
                    Id = schedule.Id,
                    StartTime = schedule.StartTime,
                    EndTime = DbFunctions.AddMinutes(schedule.StartTime, schedule.Duration),
                    ChannelId = schedule.ChannelId,
                    ShowId = schedule.Show.Id,
                    ShowName = schedule.Show.Name,
                    CategoryTag = schedule.Show.Category.Tag,
                    MovieGenre = schedule.Show.MovieGenre,
                    ImdbRating = schedule.Show.ImdbRating,
                    StarImage = schedule.Show.RatingIcon,
                }).ToList()
            });

            return View(channelList);
        }
        
        [ActionName("FilterSchedule")]
        public ActionResult Index(string Filter)
        {
            var filterList = db.Channel.Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && s.Show.Category.Name.Contains(Filter)).Select(schedule => new ScheduleVM
                {
                    Id = schedule.Id,
                    StartTime = schedule.StartTime,
                    ChannelId = schedule.ChannelId,
                    ShowName = schedule.Show.Name,
                    CategoryTag = schedule.Show.Category.Tag,
                    MovieGenre = schedule.Show.MovieGenre,
                    ImdbRating = schedule.Show.ImdbRating,
                    StarImage = schedule.Show.RatingIcon
                }).ToList()
            });

            return PartialView("_TvSchedules", filterList);
        }

        public ActionResult ShowInfo(int Id)
        {
            List<ScheduleVM> scheduleList = db.Schedule.Where(x => x.Id == Id).Select(x => new ScheduleVM
            {
                StartTime = x.StartTime,
                EndTime = DbFunctions.AddMinutes(x.StartTime, x.Duration),
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
        #endregion

        #region Popular Content
        public ActionResult PopIndex()
        {
            var popList = db.PopularContent.Select(x => new PopVM
            {
                Id = x.Id,
                ImgUrl = x.ImgUrl,
                ImgTitle = x.ImgTitle,
                Icon = x.Icon,
                ChannelId = x.ChannelId,
                ChannelName = x.Channel.Name
            }).ToList();

              return PartialView("_PopularContent", popList);
        }
        #endregion

    }
}
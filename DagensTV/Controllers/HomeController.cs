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
                ImgUrl = x.Name + ".png",
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id).Select(schedule => new ScheduleVM
                {
                    Id = schedule.Id,
                    StartTime = schedule.StartTime,
                    ChannelId = schedule.ChannelId,                    
                    ShowName = schedule.Show.Name,
                    CategoryTag = schedule.Show.Category.Tag,
                    MovieGenre = schedule.Show.MovieGenre,
                    ImdbRating = schedule.Show.ImdbRating
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
                ImgUrl = x.Name + ".png",
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id && s.Show.Category.Name.Contains(Filter)).Select(schedule => new ScheduleVM
                {
                    Id = schedule.Id,
                    StartTime = schedule.StartTime,
                    ChannelId = schedule.ChannelId,
                    ShowName = schedule.Show.Name,
                    CategoryTag = schedule.Show.Category.Tag,
                    MovieGenre = schedule.Show.MovieGenre,
                    ImdbRating = schedule.Show.ImdbRating
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
                ImdbRating = x.Show.ImdbRating
            }).ToList();

            ViewBag.ScheduleList = scheduleList;

            return PartialView("_Overlay");
        }
        #endregion

        #region Popular Content
        public ActionResult PopIndex()
        {
            // Hårdkodat tillsvidare
            List<PopularContent> popList = new List<PopularContent>
            {
                new PopularContent { ImgUrl = "../img/resevil.jpg", ImgTitle = "Resident Evil: Apocalypse", Icon = "mdi mdi-movie", Channel = "TV6" },
                new PopularContent { ImgUrl = "../img/startrek.jpg", ImgTitle = "Star Trek: Discovery", Icon = "mdi mdi-cast-connected", Channel = "Netflix" },
                new PopularContent { ImgUrl = "../img/skavlan.jpg", ImgTitle = "Skavlan", Icon = "mdi mdi-television", Channel = "SVT1" },
                new PopularContent { ImgUrl = "../img/idoljuryn.jpg", ImgTitle = "Idol 2017", Icon = "mdi mdi-television", Channel = "TV4" }
            };

            return PartialView("_PopularContent", popList);
        }
        #endregion        
    }
}

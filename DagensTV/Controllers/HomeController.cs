﻿using System;
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
            var today = new DateTime(2017, 11, 09, 06, 00, 00); //Hårdkodat tills vidare så att den tror vi är på 9e idag
            var channelList = db.Channel.Select(x => new ChannelVM
            {
                Id = x.Id,
                Name = x.Name,
                ImgUrl = x.LogoFilePath,
                Schedules = db.Schedule.Where(s => s.ChannelId == x.Id/* && s.StartTime.Value.Day == today.Day*//* && DbFunctions.AddMinutes(s.StartTime, s.Duration) > DateTime.Now*/).Select(schedule => new ScheduleVM
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
                    HasPassed = DbFunctions.AddMinutes(schedule.StartTime, schedule.Duration) < DateTime.Now,
                    IsActive = schedule.StartTime < DateTime.Now && DbFunctions.AddMinutes(schedule.StartTime, schedule.Duration) > DateTime.Now
                }).ToList()
            });

            // Används inte ännu
            //var date = DateTime.Now;
            //var dates = new List<string>
            //{
            //    date.ToShortDateString(),
            //    date.AddDays(1).ToShortDateString(),
            //    date.AddDays(2).ToShortDateString(),
            //    date.AddDays(3).ToShortDateString(),
            //    date.AddDays(4).ToShortDateString(),
            //    date.AddDays(5).ToShortDateString(),
            //    date.AddDays(6).ToShortDateString(),
            //};

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
﻿using DagensTV.Authority;
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

        // GET: Admin
        
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
            //pop.Popular = db.PopularContent.OrderBy(s => s.Id).Select(x => new PopVM
            //{
            //    Id = x.Id,
            //    ImgUrl = x.ImgUrl,
            //    Name = x.Schedule.Show.Name,
            //    Icon = x.Icon,
            //    ScheduleId = x.ScheduleId,
            //    Schedules = db.Schedule.Where(s => s.Id == x.ScheduleId).Select(schedule => new ScheduleVM
            //    {
            //        Id = schedule.Id,
            //        StartTime = schedule.StartTime,
            //        ChannelName = schedule.Channel.Name,
            //        ShowName = schedule.Show.Name,
            //        ChannelId = schedule.ChannelId
            //    }).ToList()
            //});

            pop.Channels = db.Channel.ToList();
           
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

            //IEnumerable<SelectListItem> channel = db.Channel
            //  .Select(c => new SelectListItem
            //  {
            //      Value = c.Id.ToString(),
            //      Text = c.Name
            //  });
            //ViewBag.Channels = channel;

            return View(pop);
        }

        [HttpPost]
        public ActionResult GetForm(PopVM model)
        {
            if (ModelState.IsValid)
            {
                int popular = model.Id;

                PopularContent pc = new PopularContent();

                pc = db.PopularContent.Find(popular);
                pc.ScheduleId = model.ScheduleId;
                pc.ImgUrl = "img/" + model.ImgUrl;
                pc.Icon = "mdi mdi-television";

                db.Entry(pc).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "En bild måste anges!");
            }

            return RedirectToAction("Index", "Admin");
        }
    }
}
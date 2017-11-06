﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DagensTV.Models;
using DagensTV.Models.ViewModels;

namespace DagensTV.Controllers
{
    public class UserController : Controller
    {
        private DagensTVEntities db = new DagensTVEntities();

        // GET: User
        [Authorize]
        [HttpGet]
        public ActionResult MyPage()
        {
            return View(db.Channel.ToList());
        }

        [HttpPost]
        public ActionResult MyPage(IEnumerable<Channel> channels)
        {
            var myChannels = channels.ToList();

            foreach(var item in myChannels)
            {
                Person.activeUser.mySettings.Add(item.MyPage);
            }

            


            return View();
        }
    }
}
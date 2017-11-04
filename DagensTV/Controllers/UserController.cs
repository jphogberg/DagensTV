﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DagensTV.Models;

namespace DagensTV.Controllers
{
    public class UserController : Controller
    {
        private DagensTVEntities db = new DagensTVEntities();

        // GET: User
        [Authorize]
        public ActionResult MyPage()
        {
            var channelList = db.Channel.ToList();

            return View(channelList);
        }


       
    }
}

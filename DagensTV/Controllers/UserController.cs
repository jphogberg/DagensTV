using System;
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
            Person p = (Person)Session["loggedInUser"];
            var myChannels = channels;

            foreach(var item in channels)
            {
                //Person.activeUser.myChannels.Add(item.MyPage);
            }

            


            return View();
        }
    }
}
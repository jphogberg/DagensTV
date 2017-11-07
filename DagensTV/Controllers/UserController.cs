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
            Settings mySet = new Settings();
            mySet = db.Settings.SingleOrDefault(x => x.PersonId == Person.activeUser.Id);

            //Person.activeUser.mySettings.Add(mySet.Svt1);



            //var channels = db.Channel.ToList();
            //foreach(var item in channels)
            //{
            //    item.MyPage
            //}

            return View(db.Channel.ToList());
        }

        [HttpPost]
        public ActionResult MyPage(IEnumerable<Channel> channels)
        {
            var myChannels = channels.ToList();

            foreach (var item in myChannels)
            {
                Person.activeUser.mySettings.Add(item.MyPage);
            }

            Settings s = new Settings();
            s.Svt1 = Person.activeUser.mySettings[0];
            s.Svt2 = Person.activeUser.mySettings[1];
            s.Tv3 = Person.activeUser.mySettings[2];
            s.Tv4 = Person.activeUser.mySettings[3];
            s.Kanal5 = Person.activeUser.mySettings[4];
            s.PersonId = Person.activeUser.Id;


            var dbSettings = db.Settings.ToList(); 
            foreach(var item in dbSettings)
            {
                if(item.PersonId == Person.activeUser.Id)
                {
                    Settings sUpdate = new Settings();
                    sUpdate = db.Settings.SingleOrDefault(x => x.Id == item.Id);
                    sUpdate = s;
                    db.SaveChanges();
                    break;
                }
                else
                {
                    db.Settings.Add(s);
                    db.SaveChanges();
                    break;
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
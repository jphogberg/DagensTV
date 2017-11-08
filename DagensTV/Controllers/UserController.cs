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
using DagensTV.Authority;

namespace DagensTV.Controllers
{
    [AuthorizeRoles("Användare")]
    public class UserController : Controller
    {
        private DagensTVEntities db = new DagensTVEntities();

        // GET: User
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

            //Extract only bool values
            foreach (var item in myChannels)
            {
                Person.activeUser.mySettings.Add(item.MyPage);
            }

            //Set chosen bool values to new instance
            Settings userSetting = new Settings();
            userSetting.Svt1 = Person.activeUser.mySettings[0];
            userSetting.Svt2 = Person.activeUser.mySettings[1];
            userSetting.Tv3 = Person.activeUser.mySettings[2];
            userSetting.Tv4 = Person.activeUser.mySettings[3];
            userSetting.Kanal5 = Person.activeUser.mySettings[4];
            userSetting.PersonId = Person.activeUser.Id;


            var dbSettings = db.Settings.ToList(); 
            foreach(var item in dbSettings)
            {
                if(item.PersonId == Person.activeUser.Id)
                {
                    Settings p = new Settings();
                    var pr = db.Settings.Where(x => x.PersonId == Person.activeUser.Id);

                    foreach(var dj in pr)
                    {
                        p.Id = dj.Id;

                    }
                    //int prevouseId = previousSetting.
                    //Update previous settings
                    Settings sUpdate = new Settings();
                    sUpdate = db.Settings.Find(p.Id);
                    sUpdate.Svt1 = userSetting.Svt1;
                    sUpdate.Svt2 = userSetting.Svt2;
                    sUpdate.Tv3 = userSetting.Tv3;
                    sUpdate.Tv4 = userSetting.Tv4;
                    sUpdate.Kanal5 = userSetting.Kanal5;
                    sUpdate.PersonId = Person.activeUser.Id;

                    db.Entry(sUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    break;
                }
                else
                {
                    //Insert first time settings
                    db.Settings.Add(userSetting);
                    db.SaveChanges();
                    break;
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
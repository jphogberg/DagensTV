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
            var channelList = db.Channel.ToList();

            return View(channelList);
        }

        [HttpPost]
        public ActionResult MyPage(IEnumerable<Channel> channels)
        {

            var myChannels = channels.ToList();
            MyChannels mc;
            List<MyChannels> trueList = new List<MyChannels>();
            List<MyChannels> falseList = new List<MyChannels>();
            //List<MyChannels> myNewChannels = new List<MyChannels>();

            foreach (var c in myChannels)
            {
                if (c.MyPage == true)
                {
                    //kolla om den finns i db, om ej lägg till
                    mc = new MyChannels();
                    mc.ChannelId = c.Id;
                    mc.PersonId = Person.activeUser.Id;
                    trueList.Add(mc);
                    //myNewChannels.Add(mc);
                }
                if (c.MyPage == false)
                {
                    mc = new MyChannels();
                    mc.ChannelId = c.Id;
                    mc.PersonId = Person.activeUser.Id;
                    falseList.Add(mc);
                    //myNewChannels.Add(mc);
                }
                //om ja ta bort
            }

            var oldList = db.MyChannels.ToList();

            if (oldList.Count > 0)
            {
                foreach (var item in oldList)
                {
                    if (item.PersonId == Person.activeUser.Id)
                    {
                        foreach (var trueCh in trueList)
                        {
                            if (item.ChannelId == trueCh.ChannelId)
                            {
                                db.MyChannels.Remove(item);
                            }
                            else
                            {
                                db.MyChannels.Add(item);
                                db.SaveChanges();
                            }
                        }

                        foreach (var falseCh in falseList)
                        {
                            if (item.ChannelId == falseCh.ChannelId)
                            {
                                db.MyChannels.Remove(item);
                            }
                            else
                            {
                                db.MyChannels.Add(item);
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        //Add new settings to db
                        foreach (var c in trueList)
                        {
                            db.MyChannels.Add(c);
                            db.SaveChanges();
                           
                        }
                        break;
                    }
                }




                //List<MyChannels> myNewChannels = new List<MyChannels>();
                //List<MyChannels> myOldChannels = new List<MyChannels>();
                //var dbList = db.MyChannels.ToList();

                //MyChannels myOld;

                //List<MyChannels> myOldChannels = new List<MyChannels>();


                //foreach (var item in dbList)
                //{
                //    if (item.PersonId == Person.activeUser.Id)
                //    {
                //        //Update
                //        var oldlist = db.MyChannels.Where(x => x.PersonId == Person.activeUser.Id);

                //        foreach(var i in oldlist)
                //        {
                //            myOldChannels.Add(i);
                //        }

                //        break;
                //    }
                //}



                //foreach (var item in dbList)
                //{
                //    if (item.PersonId != Person.activeUser.Id)
                //    {
                //        //Add new settings to db
                //        foreach (var c in myNewChannels)
                //        {
                //            db.MyChannels.Add(c);
                //            db.SaveChanges();
                //        }
                //        break;
                //    }
                //}
            }
            else
            {
                //Add new settings to db
                foreach (var c in trueList)
                {
                    db.MyChannels.Add(c);
                    db.SaveChanges();
                }
            }
            //foreach(var item in myChannels)
            //{
            //    if(item.MyPage == true)
            //    {
            //        Person.activeUser.myChannels.Add(item);
            //    }
            //}

            //var list = db.MyChannels.ToList();

            //foreach (var item in list)
            //{
            //    if (item.PersonId == Person.activeUser.Id)
            //    {
            //        //MyChannels mc = new MyChannels();
            //        //var dbMyC = db.MyChannels.Where(x => x.PersonId == Person.activeUser.Id && x.ChannelId == item.ChannelId);

            //        //foreach (var row in dbMyC)
            //        //{
            //        //    mc.PersonId = row.PersonId;
            //        //    mc.ChannelId = row.ChannelId;

            //        //}
            //        ////int prevouseId = previousSetting.
            //        ////Update previous settings
            //        //MyChannels mcUpdate = new MyChannels();
            //        //mcUpdate = db.Settings.Find(mc.PersonId && mc.ChannelId);
            //        //mcUpdate.Svt1 = userSetting.Svt1;

            //        //sUpdate.PersonId = Person.activeUser.Id;

            //        //db.Entry(sUpdate).State = EntityState.Modified;
            //        //db.SaveChanges();
            //        //break;
            //    }
            //    else
            //    {
            //        //Insert first time settings
            //        //db.MyChannels.Add(userSetting);
            //        //db.SaveChanges();
            //        //break;
            //    }
            //}

            ////Extract only bool values
            //foreach (var item in myChannels)
            //{
            //    Person.activeUser.mySettings.Add(item.MyPage);
            //}

            ////Set chosen bool values to new instance
            //Settings userSetting = new Settings();
            //userSetting.Svt1 = Person.activeUser.mySettings[0];
            //userSetting.Svt2 = Person.activeUser.mySettings[1];
            //userSetting.Tv3 = Person.activeUser.mySettings[2];
            //userSetting.Tv4 = Person.activeUser.mySettings[3];
            //userSetting.Kanal5 = Person.activeUser.mySettings[4];
            //userSetting.PersonId = Person.activeUser.Id;


            //var dbSettings = db.Settings.ToList();
            //foreach (var item in dbSettings)
            //{
            //    if (item.PersonId == Person.activeUser.Id)
            //    {
            //        Settings p = new Settings();
            //        var pr = db.Settings.Where(x => x.PersonId == Person.activeUser.Id);

            //        foreach (var dj in pr)
            //        {
            //            p.Id = dj.Id;

            //        }
            //        //int prevouseId = previousSetting.
            //        //Update previous settings
            //        Settings sUpdate = new Settings();
            //        sUpdate = db.Settings.Find(p.Id);
            //        sUpdate.Svt1 = userSetting.Svt1;
            //        sUpdate.Svt2 = userSetting.Svt2;
            //        sUpdate.Tv3 = userSetting.Tv3;
            //        sUpdate.Tv4 = userSetting.Tv4;
            //        sUpdate.Kanal5 = userSetting.Kanal5;
            //        sUpdate.PersonId = Person.activeUser.Id;

            //        db.Entry(sUpdate).State = EntityState.Modified;
            //        db.SaveChanges();
            //        break;
            //    }
            //    else
            //    {
            //        //Insert first time settings
            //        db.Settings.Add(userSetting);
            //        db.SaveChanges();
            //        break;
            //    }
            //}

            return RedirectToAction("Index", "Home");
        }
    }
}
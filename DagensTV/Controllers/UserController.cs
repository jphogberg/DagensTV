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

            //bool - kolla om pers finns i db

            var oldList = db.MyChannels.ToList();

            foreach(var trueCh in trueList)
            {
                foreach(var item in oldList)
                {
                    if(item.PersonId == Person.activeUser.Id && item.ChannelId != trueCh.ChannelId)
                    {
                        db.MyChannels.Add(trueCh);
                        db.SaveChanges();
                        
                    }
                    break;
                }
            }

            foreach (var falseCh in falseList)
            {
                foreach (var item in oldList)
                {
                    if (item.PersonId == Person.activeUser.Id && item.ChannelId == falseCh.ChannelId)
                    {
                        var myOld = db.MyChannels.Where(x => x.PersonId == Person.activeUser.Id && x.ChannelId == item.ChannelId);
                        foreach (var oldKey in myOld)
                        {
                            db.MyChannels.Remove(oldKey);
                            break;
                        }
                        db.SaveChanges();
                    }
                }
            }
            //jämför true list med db
            //om finns i true men ej i db = add
            //om finns i false och i db = remove
            //om finns i true och db = nada
            //om i false men ej i db = nada


           

            return RedirectToAction("Index", "Home");
        }
    }
}
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
using DagensTV.Data;

namespace DagensTV.Controllers
{
    [AuthorizeRoles("Användare", "Redaktör")]
    public class UserController : Controller
    {
        private DagensTVEntities db = new DagensTVEntities();
        private DbOperations dbo = new DbOperations();

        // GET: User
        public ActionResult MyPage()
        {
            var channelList = db.Channel.ToList();

            return View(channelList);
        }

        [HttpPost]
        public ActionResult MyPage(IEnumerable<Channel> channels)
        {

            #region Get user input
            var myChannels = channels.ToList();
            MyChannels mc;
            List<MyChannels> trueList = new List<MyChannels>();
            List<MyChannels> falseList = new List<MyChannels>();

            foreach (var c in myChannels)
            {
                if (c.MyPage == true)
                {
                    mc = new MyChannels();
                    mc.ChannelId = c.Id;
                    mc.PersonId = Person.activeUser.Id;
                    trueList.Add(mc);
                }
                if (c.MyPage == false)
                {
                    mc = new MyChannels();
                    mc.ChannelId = c.Id;
                    mc.PersonId = Person.activeUser.Id;
                    falseList.Add(mc);
                }
            }
            #endregion

            #region Set MyChannels
            bool hasMyChannels = dbo.CheckUserHasMyChannels();
            if (hasMyChannels)
            {
                dbo.UpdateTrueChannels(trueList);
                dbo.UpdateFalseChannels(falseList);
            }
            else
            {
                dbo.AddNewUserSettings(trueList);
            }
            #endregion

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyFavorites()
        {


            return View();
        }
    }
}
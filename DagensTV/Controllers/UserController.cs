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
            var allShows = db.Show.OrderBy(x => x.Name).ToList();
            return PartialView("_MyFavorites", allShows);
        }

        [HttpPost]
        public ActionResult MyFavorites(IEnumerable<Show> shows)
        {
            #region Get user input
            var myShows = shows.ToList();
            MyFavorites mf;
            List<MyFavorites> trueList = new List<MyFavorites>();
            List<MyFavorites> falseList = new List<MyFavorites>();

            foreach (var s in myShows)
            {
                if (s.MyProgram == true)
                {
                    mf = new MyFavorites();
                    mf.ShowId = s.Id;
                    mf.PersonId = Person.activeUser.Id;
                    mf.Style = "myfavorite-show";
                    trueList.Add(mf);
                }
                if (s.MyProgram == false)
                {
                    mf = new MyFavorites();
                    mf.ShowId = s.Id;
                    mf.PersonId = Person.activeUser.Id;
                    mf.Style = "";
                    falseList.Add(mf);
                }
            }
            #endregion

            #region Set MyFavorites
            bool hasMyFavorites = dbo.CheckUserHasMyFavorites();
            if (hasMyFavorites)
            {
                dbo.UpdateTrueShows(trueList);
                dbo.UpdateFalseShows(falseList);
            }
            else
            {
                dbo.AddNewUserFavoritesSettings(trueList);
            }
            #endregion

            return RedirectToAction("MyPage", "User");
        }
    }
}
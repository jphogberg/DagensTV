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
        public ActionResult MyPage()
        {
            var channelList = db.Channel.ToList();

            return View(channelList);
        }

        [HttpPost]
        public ActionResult SetMyPage(MyPageVM mypage)
        {
            try
            {
                Channel c;
                foreach (var channel in mypage.Channels)
                {
                    c = new Channel();
                    var dbC = db.Channel.Where(x => x.Id.Equals(channel.Id));
                    if (dbC != null)
                    {

                        //dbC.MyPage = channel.MyPage;
                        
                    }
                }
                //db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
              
            return RedirectToAction("Index", "Home");
        }
       
    }
}

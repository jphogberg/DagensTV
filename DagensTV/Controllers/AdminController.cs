using DagensTV.Models;
using DagensTV.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Controllers
{
    
    public class AdminController : Controller
    {
        DagensTVEntities db = new DagensTVEntities();

        // GET: Admin
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {

            var popularContent = db.PopularContent.Select(x => new PopVM
            {
                Id = x.Id,
                ImgUrl = x.ImgUrl,
                ImgTitle = x.ImgTitle,
                Icon = x.Icon,
                ChannelName = x.Channel.Name,
                
            });

            var channelList = db.Channel.ToList();
            ViewBag.PopChannelList = channelList;
            return View(popularContent);
        }

        /*Channels = db.Channel.ToList()*/ /*Where(c => c.Id == x.Channel.Id).Select(channel => new Channel
                //{
                //    Id = channel.Id,
                //    Name = channel.Name
                //}).ToList()*/

        [HttpPost]
        public ActionResult GetForm(PopVM model)
        {
           


            return View();
        }
    }
}
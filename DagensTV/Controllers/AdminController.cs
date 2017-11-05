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
                Channels = db.Channel.Where(s => s.Id == x.Id).Select(channel => new ChannelVM
                {
                    Id = channel.Id,
                    Name = channel.Name
                }).ToList()
            });

            return View(popularContent);
        }

        [HttpPost]
        public ActionResult GetForm(PopVM model)
        {
           


            return View();
        }
    }
}
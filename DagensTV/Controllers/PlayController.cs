using DagensTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Controllers
{
    public class PlayController : Controller
    {
        // GET: Play
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopPlay()
        {
            // Hårdkodat tillsvidare
            List<PopularContent> popList = new List<PopularContent>
            {
                new PopularContent { ImgUrl = "../img/detroit.jpg", ImgTitle = "Detroit Red Wings - Tampa Bay Lightning", Icon = "mdi mdi-cast-connected", Channel = "Viaplay" },
                new PopularContent { ImgUrl = "../img/startrek.jpg", ImgTitle = "Star Trek: Discovery", Icon = "mdi mdi-cast-connected", Channel = "Netflix" },
                new PopularContent { ImgUrl = "../img/skavlan.jpg", ImgTitle = "Skavlan", Icon = "mdi mdi-cast-connected", Channel = "SVTPlay" },
                new PopularContent { ImgUrl = "../img/idoljuryn.jpg", ImgTitle = "Idol 2017", Icon = "mdi mdi-cast-connected", Channel = "TV4 Play" }
            };

            return PartialView("_PopularContent", popList);
        }
    }
}
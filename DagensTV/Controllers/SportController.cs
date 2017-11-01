using DagensTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Controllers
{
    public class SportController : Controller
    {
        // GET: Sport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopSport()
        {
            // Hårdkodat tillsvidare
            List<PopularContent> popList = new List<PopularContent>
            {
                new PopularContent { ImgUrl = "../img/detroit.jpg", ImgTitle = "Detroit Red Wings - Tampa Bay Lightning", Icon = "mdi mdi-checkbox-blank-circle", Channel = "Viasat Hockey" },
                new PopularContent { ImgUrl = "../img/hammarby.jpg", ImgTitle = "Kalmar FF - Hammarby IF", Icon = "mdi mdi-soccer", Channel = "C More Fotboll" },
                new PopularContent { ImgUrl = "../img/snooker.jpg", ImgTitle = "English Open 2017: Dag 1", Icon = "mdi mdi-auto-fix", Channel = "Eurosport 2" },
                new PopularContent { ImgUrl = "../img/titans.jpg", ImgTitle = "Tennessee Titans - Indianapolis Colts", Icon = "mdi mdi-football", Channel = "TV3 Sport" }
            };

            return PartialView("_PopularContent", popList);
        }
    }
}
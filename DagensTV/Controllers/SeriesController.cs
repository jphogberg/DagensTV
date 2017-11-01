using DagensTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Controllers
{
    public class SeriesController : Controller
    {
        // GET: Series
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopSeries()
        {
            // Hårdkodat tillsvidare
            List<PopularContent> popList = new List<PopularContent>
            {
                new PopularContent { ImgUrl = "../img/himym.jpg", ImgTitle = "How I met your mother", Icon = "mdi mdi-television", Channel = "TV6" },
                new PopularContent { ImgUrl = "../img/startrek.jpg", ImgTitle = "Star Trek: Discovery", Icon = "mdi mdi-cast-connected", Channel = "Netflix" },
                new PopularContent { ImgUrl = "../img/skavlan.jpg", ImgTitle = "Skavlan", Icon = "mdi mdi-television", Channel = "SVT1" },
                new PopularContent { ImgUrl = "../img/idoljuryn.jpg", ImgTitle = "Idol 2017", Icon = "mdi mdi-television", Channel = "TV4" }
            };

            return PartialView("_PopularContent", popList);
        }
    }
}
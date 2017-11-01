using DagensTV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DagensTV.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PopMovies()
        {
            // Hårdkodat tillsvidare
            List<PopularContent> popList = new List<PopularContent>
            {
                new PopularContent { ImgUrl = "../img/resevil.jpg", ImgTitle = "Resident Evil: Apocalypse", Icon = "mdi mdi-movie", Channel = "TV6" },
                new PopularContent { ImgUrl = "../img/lotr.jpg", ImgTitle = "Sagan om Ringen", Icon = "mdi mdi-movie", Channel = "TV3" },
                new PopularContent { ImgUrl = "../img/alien.jpg", ImgTitle = "Alien", Icon = "mdi mdi-movie", Channel = "Kanal 5" },
                new PopularContent { ImgUrl = "../img/terminator.jpg", ImgTitle = "Terminator: Genisys", Icon = "mdi mdi-movie", Channel = "TV4" }
            };

            return PartialView("_PopularContent", popList);
        }
    }
}
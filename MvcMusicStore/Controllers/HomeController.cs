using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();

        public ActionResult Index()
        {
            var albums = GetLatestAlbums(7);
            return View(albums);
        }

        public ActionResult Search(string q)
        {
            var albums = db.Albums
                .Include("Artist")
                .Where(a => a.Artist.Name.Contains(q) || a.Title.Contains(q))
                .Take(10);
            return View(albums);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Album> GetLatestAlbums(int count)
        {
            //Get a list of albums but only a specific amount and then convert to a list
            return db.Albums.Take(count).ToList();
        }

        public ActionResult DailyDeal()
        {
            var album = GetDailyDeal();
            return PartialView("_DailyDeal", album);
        }
        public Album GetDailyDeal()
        {
          return db.Albums.OrderBy(a => a.Price).First();
        }
    }
}

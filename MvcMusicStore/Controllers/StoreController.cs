using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        //create connection to MusicStoreDB
        private MusicStoreDBContext db = new MusicStoreDBContext();

        //GET: /Store/Browse?genre=Disco
        public ActionResult Browse(string genre)
        {
            var genreModel = db.Genres.Include("Albums").Single(g => g.Name == genre);
            return View(genreModel);
          /*
           * HttpUtility.HtmlEncode utility method used to sanitize user input.
           * This prevents users from injecting JavaScript code or HTML
           * markup into our view with a link like 
           * /Store/Browse?Genre=<script>window.location='http://
           * hacker.example.com'</script>
           */
            //string message = HttpUtility.HtmlEncode("Store.Browse, Genre = " + genre);
          //return message;
          //after this show example of /Store/Browse?genre=Rap
        }

        //GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            var album = db.Albums.Find(id);
            return View(album);
            //string message = "Store.Details, ID = " + id;
          //return message;
          //browse to /Store/Details/5
        }

        //ChildActionOnly attribute prevents runtime from invoking the action directly in the URL. Used when isolating an action method that will be called within a page
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = db.Genres.ToList();
            return PartialView(genres);
        }
    }
}

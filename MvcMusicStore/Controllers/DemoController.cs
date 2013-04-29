using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcMusicStore.Models;
using System.Data;

namespace MvcMusicStore.Controllers
{
    public class DemoController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();
        //
        // GET: /Demo/

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu(MenuOptions options)
        {
            //get a list of all artists in the artists table
            //var menu = GetArtists();
            //user the menuOptions class to hold artists
            options.Artists = GetArtists();
            return PartialView(options);
        }

        private List<Artist> GetArtists()
        {
            return db.Artists.ToList();
        }

        [HttpPost]
        public ActionResult HelpersExample(Album album)
        {
            //var album = db.Albums.Find(id);
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Title", "Title is not valid!");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        public ActionResult HelpersExample(int id = 0)
        {
            ViewBag.Message = id.ToString();
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Price = 10.0;

            //ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
          ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);

          ViewBag.GenreID = db.Genres
              .OrderBy(g => g.Name)
              .AsEnumerable()
              .Select(g => new SelectListItem
              {
                  Text = g.Name,
                  Value = g.GenreID.ToString(),
                  Selected = album.GenreID == g.GenreID
              });

            return View(album);
        }

    }
}

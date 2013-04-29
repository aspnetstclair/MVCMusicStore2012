using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ArtistController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();

        //
        // GET: /Artist/

        public ActionResult Index()
        {
            var artists = db.Artists.ToList();
            return View(artists);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        public ActionResult Edit(int id)
        {
            Artist artist = db.Artists.Find(id);
            return View(artist);
        }

        [HttpPost]
        public ActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        [ChildActionOnly]
        public ActionResult Preview()
        {
            var artist = GetLatestAlbum();
            return PartialView(artist);
        }

        private Album GetLatestAlbum()
        {
            return db.Albums.FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();
        //
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
            //get the current cart in relation to the logged in user
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //create shopping cart view model object to hold shopping cart info
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
          };
          //return view
          return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            //retrieve the album from the database
            var addedAlbum = db.Albums.Single(album => album.AlbumID == id);
            //add to the shoppingcart that album
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedAlbum);
            //go back to the main index page
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //create a ViewData array item named 
            //CartCount and place the int of total
            //records
            ViewData["CartCount"] = cart.GetCount();
            //load cartSummary partial view
            return PartialView("CartSummary");
        }

    }
}

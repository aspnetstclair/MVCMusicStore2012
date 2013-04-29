using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();
        //constant variable holding our promocode
        private const string PromoCode = "FREE";

        //get
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        //Post
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            //associate order related form fields with the order object
            var order = new Order();
            //fire validation now
            TryUpdateModel(order);
            try
            {
                //if the value of the promocode in the textbox is equal to "Free"
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    //set missing property values left owing in order object
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    //save the order
                    db.Orders.Add(order);
                    db.SaveChanges();

                    //process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                   cart.CreateOrder(order);

                    //instead of returning just a view, we will instead return an action method
                   return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch
            {
                return View(order);
            }
        }

        //Get -- /Checkout/Complete
        public ActionResult Complete(int id)
        {
            //Validate that customer owns this order
            bool isValid = db.Orders.Any(o => o.OrderId == id && o.Username == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

    }
}
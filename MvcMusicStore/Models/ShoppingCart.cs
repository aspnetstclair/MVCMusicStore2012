using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Models
{
    //a partial class allows developers to split up the class file
    public partial class ShoppingCart
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();
        private string ShoppingCartId { get; set; }

        //const = constant, a variable that is unchangable during runtime
        public const string CartSessionKey = "CartId";

        public string GetCartId(HttpContextBase context)
        {
            //check if our session variable for the cart exists
            if (context.Session[CartSessionKey] == null)
            {
                //check if a user is logged in by seeing if their username exists
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    //if logged in assign user's name to cart session key
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else //user does not exist or has never logged in
                {
                    //generrate a temporary cart id for user
                    Guid tempCartId = Guid.NewGuid();
                    //assign temp cart id to session variable
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

      /*This version of GetCart is passed the current HttpContextBase which represents
       * the currect running application so that we can get to session and membership 
       * information. Note that it must return a shoppingCart object*/
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            //create an object of ShopingCart
            var cart = new ShoppingCart();
            //get the shopping cart id of logged user
            cart.ShoppingCartId = cart.GetCartId(context);
            //return fully initalized shopping cart object
            return cart;
        }

        /*From the controller this version of GetCart is called first.
        note that we are getting current context on the site by referring to 
        controller.HttpContext. This allows us access to Session and Membership
        logged in user info etc...). By making 2 versions of the GetCart method
        we can call the version below from a controller or call it as it is
        above using HttpContextBase. that this version returns the shopping cart object 
         created in the other version of GetCart*/
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void EmptyCart()
        {
            //select all cart items linked to your shopping cart id
            var cartItems = db.Carts.Where(cart => cart.CartId == ShoppingCartId);
            //loop through those shopping cart items 1 at a time
            foreach( var cartItem in cartItems)
            {
                //for each found cart item remove from the table
                db.Carts.Remove(cartItem);
            }
            //save changes to the database
            db.SaveChanges();
        }

        //method used to return a list of items in my shopping cart
        public List<Cart> GetCartItems()
        {
            //returns select from carts where cart id is yours to a list (array)
            return db.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0; //variable used to hold our total
            var cartItems = GetCartItems(); //get all of our items from the Cart table
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                { //filling out our order detail records 
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };
                //set the order total of the shopping cart
                orderTotal += (item.Count * item.Album.Price);
                db.OrderDetails.Add(orderDetail);
            }
            order.Total = orderTotal; //set the order table's order column to the cost of all albums
            db.SaveChanges(); //save all changes to the database
            EmptyCart();
            return order.OrderId; //returns the orderID as a confirmation number
        }

        public int GetCount() 
        {
            //get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Carts where cartItems.CartId == ShoppingCartId select (int?)cartItems.Count).Sum();
            //return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        { 
            //multiply album price by count of albums to get the current price
            //for each albums in the cart
            decimal? total = (from cartItems in db.Carts where cartItems.CartId == ShoppingCartId select (int?)cartItems.Count * cartItems.Album.Price).Sum();
            //if the var total holds a null value, assign the proper zero decimal  0.0
            return total ?? decimal.Zero;
        }

        public void AddToCart(Album album)
        {
            //get the matching cart and album instances
            var cartItem = db.Carts
                .SingleOrDefault(c => c.CartId == ShoppingCartId && c.AlbumId == album.AlbumID);

            if (cartItem == null)
            {
                //create a new item if no cart items exists
                cartItem = new Cart
                {
                    AlbumId = album.AlbumID,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                db.Carts.Add(cartItem);
            }
            else
            { 
                //if the item does exist in the cart then add one to quanity
                cartItem.Count++;
            }
            //save changes to database
            db.SaveChanges();
        }

    }
}
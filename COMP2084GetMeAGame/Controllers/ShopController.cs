using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP2084GetMeAGame.Data;
using COMP2084GetMeAGame.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace COMP2084GetMeAGame.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(categories);
        }
        public IActionResult Browse(int id)
        {
            // get the products in the selected category
            var products = _context.Products.Where(p => p.CategoryId == id).OrderBy(p => p.Name).ToList();
            // load the Browse page and pass it the list of products to display
            return View(products);
        }

        // GET: /Shop/AddToCart
        [HttpPost]
        public IActionResult AddToCart(int ProductId, int Quantity)
        {
            // get current price of the product
            var price = _context.Products.Find(ProductId).Price;

            // identify the customer
            var customerId = GetCustomerId();

            // check if product already exists in this user's cart
            //var cartItem = _context.Carts.SingleOrDefault(c => c.ProductId == ProductId && c.CustomerId == customerId);

            //if (cartItem != null)
            //{
            //    // product already exists so update the quantity
            //    cartItem.Quantity += Quantity;
            //    _context.Update(cartItem);
            //    _context.SaveChanges();
            //}
            //else
            //{
            // create a new Cart object
            var cart = new Cart
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    Price = price,
                    CustomerId = customerId,
                    DateCreated = DateTime.Now
                };

                // use the Carts DbSet in ApplicationContext.cs to save to the database
                _context.Carts.Add(cart);
                _context.SaveChanges();
            //}

            // redirect to show the current cart
            return RedirectToAction("Cart");
        }

        private string GetCustomerId()
        {
            // is there already a session variable holding an identifier for this customer?
            if (HttpContext.Session.GetString("CustomerId") == null)
            {
                // cart is empty, user is unknown
                var customerId = "";

                // use a Guid to generate a new unique identifier
                customerId = Guid.NewGuid().ToString();

                // now store the new identifier in a session variable
                HttpContext.Session.SetString("CustomerId", customerId);
            }

            // return the CustomerId to the AddToCart method
            return HttpContext.Session.GetString("CustomerId");
        }

        // GET: /Shop/Cart
        public IActionResult Cart()
        {
            // get CustomerId from the session variable
            var customerId = HttpContext.Session.GetString("CustomerId");

            // get items in this customer's cart - add reference to the parent object: Product
            var cartItems = _context.Carts.Include(c => c.Product).Where(c => c.CustomerId == customerId).ToList();

            // count the # of items in the Cart and write to a session variable to display in the navbar
            var itemCount = (from c in _context.Carts
                             where c.CustomerId == customerId
                             select c.Quantity).Sum();
            HttpContext.Session.SetInt32("ItemCount", itemCount);

            // load the cart page and display the customer's items
            return View(cartItems);
        }
    }
}

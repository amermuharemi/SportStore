using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SportStore.Infrastructure;
using SportStore.Interfaces;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repo;

        public CartController(IProductRepository _repo)
        {
            repo = _repo;
        }

        public ViewResult Index(string returnURl)
        {
            return View(new CartIndexViewModel
            {
                Cart = new Cart(),
                ReturnUrl = returnURl
            });
        } 

        public RedirectToActionResult AddToCart(int productId,string returnUrl)
        {
            Product product = repo.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productID, string returnUrl)
        {
            Product product = repo.Products
                .FirstOrDefault(x => x.ProductID == productID);

            if (product == null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        public void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
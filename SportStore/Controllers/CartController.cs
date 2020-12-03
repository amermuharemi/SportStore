using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Interfaces;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repo;

        public CartController(IProductRepository _repo)
        {
            repo = _repo;
        }
        
        public RedirectToActionResult AddToCart(int productId,string returnUrl)
        {
            Product product = repo.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product == null)
            {
                Cart cart = GetCart();
            }
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
    }
}
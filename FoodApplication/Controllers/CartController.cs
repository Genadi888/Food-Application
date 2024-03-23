using Microsoft.AspNetCore.Mvc;
using FoodApplication.Repository;
using FoodApplication.ContextDBConfig;
using FoodApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace FoodApplication.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IData data;
        private readonly FoodDbContext context;
        public CartController(IData data, FoodDbContext context) 
        {
            this.data = data;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveCart(Cart cart)
        {
            var user = await data.GetUser(HttpContext.User);
            cart.UserId = user?.Id;
            if (ModelState.IsValid)
            {
                context.Carts.Add(cart);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAddedCarts()
        {
            var user = await data.GetUser(HttpContext.User);
            List<string?> carts = context.Carts.Where(c => c.UserId == user.Id).Select(c => c.RecipeId).ToList();
            return Ok(carts);
        }

        [HttpPost]
        public IActionResult RemoveCartFromList(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var cart = context.Carts.Where(c => c.RecipeId == Id).FirstOrDefault();
                if (cart!=null)
                {
                    context.Carts.Remove(cart);
					context.SaveChanges();
					return Ok();
				}
			}
            return BadRequest();
        }

        [HttpGet]

        public async Task<IActionResult> GetCartList()
        {
            var user = await data.GetUser(HttpContext.User);
            var cartList = context.Carts.Where(c => c.UserId == user.Id).ToList();
            return View();
        }
    }
}

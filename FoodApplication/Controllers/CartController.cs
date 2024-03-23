using Microsoft.AspNetCore.Mvc;
using FoodApplication.Repository;
using FoodApplication.ContextDBConfig;
using FoodApplication.Models;

namespace FoodApplication.Controllers
{
    [Authorise]
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
            var carts = context.Carts.Where(c => c.UserId == user.Id).Select(c => c.RecipeId).ToList();
            return Ok(carts);
        }
    }
}

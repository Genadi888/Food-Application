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
    }
}

using FoodApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Controllers
{
    public class RecipeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetRecipeCard([FromBody] List<Recipe> recipe)
        {
            return PartialView("_RecipeCard", recipe);
        }

		[HttpPost]
		public IActionResult ShowOrder(OrderRecipeDetails details)
		{
			return PartialView("_ShowOrder", details);
		}
    }
}

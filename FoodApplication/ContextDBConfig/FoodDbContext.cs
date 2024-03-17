using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.ContextDBConfig
{
	public class FoodDbContext : IdentityDbContext<IdentityUser>
	{
		public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) 
		{ 
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

	}
}

﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FoodApplication.Models;

namespace FoodApplication.ContextDBConfig
{
	public class FoodDbContext : IdentityDbContext<ApplicationUser>
	{
		public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options) 
		{ 
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

		public DbSet<Order> Orders { get; set; }

	}
}

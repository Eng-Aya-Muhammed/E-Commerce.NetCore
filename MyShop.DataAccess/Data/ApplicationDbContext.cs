﻿//using Microsoft.EntityFrameworkCore;
//using MyShop.Entites.Models;
//using MyShop.Entities.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Entites.Models;
using MyShop.Entities.Models;

namespace MyShop.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>


    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

	}
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Data
{
    public class AppDBContent : IdentityDbContext<IdentityUser>
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {
            
        }
        public DbSet<Car> Cars{get;set;}
        public DbSet<Category> Categories{get;set;}
        public DbSet<ShopCartItem> ShopCartItems { get; set; }

    }
}

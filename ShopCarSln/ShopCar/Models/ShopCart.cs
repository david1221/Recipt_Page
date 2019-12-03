using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopCar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCar.Models
{
    public class ShopCart
    {
        private readonly AppDBContent _appDBContent;
        public ShopCart(AppDBContent appDBContent)
        {
            _appDBContent = appDBContent;
        }
        public string ShopCartId { get; set; }
        public List<ShopCartItem> ListShopItems { get; set; }

        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var content = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", shopCartId);
            return new ShopCart(content) { ShopCartId = shopCartId };
        }
        public void  AddToCart(Car car)
        {
            _appDBContent.ShopCartItems.Add(new ShopCartItem
            {
                Car = car,
                ShopCartId = ShopCartId,
                Price = car.Price
            }
            );
            _appDBContent.SaveChanges();
        }
        public List<ShopCartItem> GetShopItems()
        {
            return _appDBContent.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(c => c.Car).ToList();
        }
    }
}

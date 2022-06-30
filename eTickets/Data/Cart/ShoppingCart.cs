using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCardItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Movies movie)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movies.Id == movie.Id && n.ShoppingCardId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCardItem()
                {
                    ShoppingCardId = ShoppingCartId,
                    Movies = movie,
                    Amount = 1
                };

                _context.ShoppingCardItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movies movie)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movies.Id == movie.Id && n.ShoppingCardId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCardItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCardItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCartId).Include(n => n.Movies).ToList());
        }

        public double GetShoppingCartTotal() => _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCartId).Select(n => n.Movies.Price * n.Amount).Sum();


        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCartId).ToListAsync();
            _context.ShoppingCardItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context;
        public string ShoppingCardId { get; set; }
        public List<ShoppingCardItem> ShoppingCardItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cardId = session.GetString("cardId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cardId);

            return new ShoppingCart(context) { ShoppingCardId = cardId };

        }

        public void AddItemToCart(Movies movie)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movies.Id == movie.Id && n.ShoppingCardId == ShoppingCardId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCardItem()
                {
                    ShoppingCardId = ShoppingCardId,
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

        public void RemoveItemFromCard(Movies movies)
        {
            var shoppingCartItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Movies.Id == movies.Id && n.ShoppingCardId == ShoppingCardId);

            if (shoppingCartItem == null)
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

        public List<ShoppingCardItem> GetShoppingCardItems()
        {
            return ShoppingCardItems ?? (ShoppingCardItems = _context.ShoppingCardItems.Where(x => x.ShoppingCardId == ShoppingCardId)
                .Include(n => n.Movies).ToList());
        }

        public double GetShoppingCartTotal() =>
                _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCardId)
                .Select(n => n.Movies.Price * n.Amount).Sum();

    }
}

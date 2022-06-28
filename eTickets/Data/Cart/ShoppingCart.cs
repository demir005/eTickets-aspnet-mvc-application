using eTickets.Models;
using Microsoft.EntityFrameworkCore;
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

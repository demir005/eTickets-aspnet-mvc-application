using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services.Interfaces
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCardItem> items, string userId, string UserEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}

using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class ShoppingCardItem
    {
        [Key]
        public int Id { get; set; }
        public Movies Movies { get; set; }
        public int Amount { get; set; }
        public string ShoppingCardId { get; set; }
    }
}

using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public IEnumerable<ShoppingCartItem> Items { get; set; }
        public decimal TotalPrice 
        {
            get
            {
                decimal total = default;

                foreach (var item in Items)
                    total += item.Price * item.Quantity;

                return total;
            }
        }
    }
}

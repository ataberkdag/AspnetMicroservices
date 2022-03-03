using System.Collections.Generic;
using System.Linq;

namespace Basket.API.Entites
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            this.UserName = username;
        }

        public decimal TotalPrice()
        {
            return ShoppingCartItems.Select(x => x.Price * x.Quantity).Sum();
        }
    }
}

namespace Uinversity.Entities
{
    public class Cart
    {
        public string UserName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public Cart()
        {
            UserName = string.Empty;
        }
        public Cart(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
    }
}
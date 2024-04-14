namespace WebBanHangLapTop.Models
{
    public class CheckoutViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        public ShoppingCart Cart { get; set; }
        public decimal Subtotal => Cart.Items.Sum(i => i.Price * i.Quantity);
    }
}

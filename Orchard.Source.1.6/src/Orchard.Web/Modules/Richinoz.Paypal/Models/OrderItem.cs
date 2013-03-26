using Richinoz.Paypal.Services;

namespace Richinoz.Paypal.Models {
    public class OrderItem : IOrderItem
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set;}
    }
}
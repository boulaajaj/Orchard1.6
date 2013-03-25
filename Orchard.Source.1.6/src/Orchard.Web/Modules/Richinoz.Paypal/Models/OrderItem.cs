namespace Richinoz.Paypal.Models {
    public class OrderItem 
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set;}
    }
}
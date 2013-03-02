namespace Richinoz.Paypal.Models {
     public class Order
    {
        public string UserName { get; set; }

        public decimal Total { get; set; }

        public string Id { get; set; }

        public string Items { get; set; }
    }
}

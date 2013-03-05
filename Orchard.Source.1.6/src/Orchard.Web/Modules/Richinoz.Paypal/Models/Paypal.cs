using System.Collections.Generic;

namespace Richinoz.Paypal.Models
{
    public class Paypal
    {
        public Paypal()
        {
            PaypalItems = new List<PaypalItem>();
        }
        public string cmd { get; set; }
        public string business { get; set; }
        public string no_shipping{ get; set; }
        public string @return{ get; set; }
        public string cancel_return{ get; set; }
        public string notify_url { get; set; }
        public string currency_code { get; set; }
        public List<PaypalItem> PaypalItems { get; set; }
    }

    public class PaypalItem
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public int Quantity { get; set; }
    }
}
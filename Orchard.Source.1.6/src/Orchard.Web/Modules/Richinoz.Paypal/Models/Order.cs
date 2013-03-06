using System;
using System.Collections.Generic;

namespace Richinoz.Paypal.Models {
    [Serializable]
    public class Order {
        public Order() {
            OrderItems= new List<OrderItem>();
        }
        public int Id { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public Address Address { get; set; }

    }

    [Serializable]
    public class OrderItem {
        public string Name { get; set; }
        public decimal Amount{ get; set; }
        public int Quantity{ get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Richinoz.Paypal.Controllers;
using Richinoz.Paypal.Services;

namespace Richinoz.Paypal.Models
{
    [Serializable]
    public class Order : IOrder
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int UniqueId { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public Address Address { get; set; }

        public decimal OriginalAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public string TransactionId { get; set; }
    }
}
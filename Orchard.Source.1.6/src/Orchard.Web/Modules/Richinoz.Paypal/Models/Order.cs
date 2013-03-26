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
        private decimal _amount;
        private decimal _amountPaid;
        private string _transactionId;
        public Order()
        {
            OrderItems = new List<IOrderItem>();
        }
        public int UniqueId { get; set; }

        public List<IOrderItem> OrderItems { get; set; }
        public IAddress Address { get; set; }

        public decimal OriginalAmount {
            get { return _amount; }
            set { _amount = value; }
        }

        public decimal AmountPaid {
            get { return _amountPaid; }
            set { _amountPaid = value; }
        }

        public string TransactionId {
            get { return _transactionId; }
            set { _transactionId = value; }
        }
    }
}
using Orchard.ContentManagement;

namespace Richinoz.Paypal.Models {
    public class OrderPart:ContentPart<OrderPartRecord> {

        public virtual string Details
        {
            get { return Record.Details; }
            set { Record.Details = value; }
        }

        public virtual decimal Amount
        {
            get { return Record.Amount; }
            set { Record.Amount = value; }
        }

        public virtual string TransactionId
        {
            get { return Record.TransactionId; }
            set { Record.TransactionId = value; }
        }

        public Order Order { get; set; }

    }
}


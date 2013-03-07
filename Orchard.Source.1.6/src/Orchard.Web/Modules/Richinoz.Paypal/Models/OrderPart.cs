using Orchard.ContentManagement;

namespace Richinoz.Paypal.Models {
    public class OrderPart:ContentPart<OrderPartRecord> {

        public string Details
        {
            get { return Record.Details; }
            set { Record.Details = value; }
        }

        public decimal Amount
        {
            get { return Record.Amount; }
            set { Record.Amount = value; }
        }

        public string TransactionId
        {
            get { return Record.TransactionId; }
            set { Record.TransactionId = value; }
        }

    }
}


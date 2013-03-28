using Orchard.ContentManagement.Records;

namespace Richinoz.Paypal.Models {
    public class OrderPartRecord:ContentPartRecord {
        
        public virtual string Details { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string TransactionId{ get; set; }
        public virtual string Status{ get; set; }
    }
}

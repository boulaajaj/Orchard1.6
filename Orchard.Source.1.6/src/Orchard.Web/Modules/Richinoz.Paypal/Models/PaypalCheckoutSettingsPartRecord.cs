using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;

namespace Richinoz.Paypal.Models {
    [OrchardFeature("Paypal.Checkout")]
    public class PaypalCheckoutSettingsPartRecord : ContentPartRecord {
        public virtual string MerchantId { get; set; }
        public virtual bool UseSandbox { get; set; }
        public virtual string Currency { get; set; }
        public virtual string WeightUnit { get; set; }
        public virtual string AnalyticsId { get; set; }
        public virtual string ReturnUrl { get; set; }
        public virtual string CancelUrl { get; set; }
        public virtual string NotifyUrl { get; set; }
    }
}

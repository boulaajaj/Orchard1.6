using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;

namespace Richinoz.Paypal.Models {
    [OrchardFeature("Paypal.Checkout")]
    public class PaypalCheckoutSettingsPart : ContentPart<PaypalCheckoutSettingsPartRecord> {
        [Required]
        public string MerchantId { get { return Record.MerchantId; } set { Record.MerchantId = value; } }
        public bool UseSandbox { get { return Record.UseSandbox; } set { Record.UseSandbox = value; } }
        [DefaultValue("AUD")]
        public string Currency { get { return Record.Currency; } set { Record.Currency = value; } }
        [DefaultValue("LB")]
        public string WeightUnit { get { return Record.WeightUnit; } set { Record.WeightUnit = value; } }
        public string AnalyticsId { get { return Record.AnalyticsId; } set { Record.AnalyticsId = value; } }
        public string ReturnUrl { get { return Record.ReturnUrl; } set { Record.ReturnUrl = value; } }
        public string CancelUrl { get { return Record.CancelUrl; } set { Record.CancelUrl = value; } }
        public string NotifyUrl { get { return Record.NotifyUrl; } set { Record.NotifyUrl = value; } }

    }
}

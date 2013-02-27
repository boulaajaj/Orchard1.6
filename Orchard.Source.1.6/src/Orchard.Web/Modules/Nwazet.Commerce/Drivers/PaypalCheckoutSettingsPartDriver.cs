using System;
using Nwazet.Commerce.Models;
using Orchard.Caching;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Environment.Extensions;

namespace Nwazet.Commerce.Drivers {
    [OrchardFeature("Paypal.Checkout")]
    public class PaypalCheckoutSettingsPartDriver : ContentPartDriver<PaypalCheckoutSettingsPart> {
        private readonly ISignals _signals;

        public PaypalCheckoutSettingsPartDriver(ISignals signals)
        {
            _signals = signals;
        }

        protected override string Prefix { get { return "PaypalCheckoutSettings"; } }

        protected override DriverResult Editor(PaypalCheckoutSettingsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_PaypalCheckout_Settings",
                               () => shapeHelper.EditorTemplate(
                                   TemplateName: "Parts/PaypalCheckoutSettings",
                                   Model: part.Record,
                                   Prefix: Prefix)).OnGroup("PaypalCheckout");
        }

        protected override DriverResult Editor(PaypalCheckoutSettingsPart part, IUpdateModel updater, dynamic shapeHelper) {
            updater.TryUpdateModel(part.Record, Prefix, null, null);
            _signals.Trigger("PaypalCheckout.Changed");
            return Editor(part, shapeHelper);
        }

        protected override void Importing(PaypalCheckoutSettingsPart part, ImportContentContext context) {
            var merchantId = context.Attribute(part.PartDefinition.Name, "MerchantId");
            if (!String.IsNullOrWhiteSpace(merchantId)) {
                part.MerchantId = merchantId;
            }
            var currency = context.Attribute(part.PartDefinition.Name, "Currency");
            if (!String.IsNullOrWhiteSpace(currency)) {
                part.Currency = currency;
            }
            var weightUnit = context.Attribute(part.PartDefinition.Name, "WeightUnit");
            if (!String.IsNullOrWhiteSpace(weightUnit)) {
                part.WeightUnit = weightUnit;
            }
            var analyticsId = context.Attribute(part.PartDefinition.Name, "AnalyticsId");
            if (!String.IsNullOrWhiteSpace(analyticsId)) {
                part.AnalyticsId = analyticsId;
            }
            var useSandboxAttribute = context.Attribute(part.PartDefinition.Name, "UseSandbox");
            bool useSandbox;
            if (Boolean.TryParse(useSandboxAttribute, out useSandbox)) {
                part.UseSandbox = useSandbox;
            }
            var returnUrl = context.Attribute(part.PartDefinition.Name, "ReturnUrl");
            if (!String.IsNullOrWhiteSpace(returnUrl))
            {
                part.ReturnUrl = returnUrl;
            }
        }

        protected override void Exporting(PaypalCheckoutSettingsPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("MerchantId", part.MerchantId);
            context.Element(part.PartDefinition.Name).SetAttributeValue("Currency", part.Currency);
            context.Element(part.PartDefinition.Name).SetAttributeValue("WeightUnit", part.WeightUnit);
            context.Element(part.PartDefinition.Name).SetAttributeValue("AnalyticsId", part.AnalyticsId);
            context.Element(part.PartDefinition.Name).SetAttributeValue("UseSandbox", part.UseSandbox.ToString().ToLower());
            context.Element(part.PartDefinition.Name).SetAttributeValue("ReturnUrl", part.ReturnUrl);
        }
    }
}

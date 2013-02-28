using System.Collections.Generic;
using Orchard.Caching;
using Orchard.DisplayManagement;
using Orchard.Environment.Extensions;
using Orchard;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services {
 
    //[OrchardFeature("Paypal.Checkout")]
    public class PaypalCheckoutService : IPaypalCheckoutService
    {
        private readonly IWorkContextAccessor _wca;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;
        private readonly dynamic _shapeFactory;

        public PaypalCheckoutService(
            IWorkContextAccessor wca, 
            ICacheManager cacheManager, 
            ISignals signals, 
            IShapeFactory shapeFactory) {

            _wca = wca;
            _cacheManager = cacheManager;
            _signals = signals;
            _shapeFactory = shapeFactory;
        }

        public PaypalCheckoutSettingsPart GetSettings()
        {
            return _cacheManager.Get(
                "PaypalCheckoutSettings",
                ctx =>
                {
                    ctx.Monitor(_signals.When("PaypalCheckout.Changed"));
                    var workContext = _wca.GetContext();
                    return (PaypalCheckoutSettingsPart)workContext
                                                  .CurrentSite
                                                  .ContentItem
                                                  .Get(typeof(PaypalCheckoutSettingsPart));
                });
        }

        public dynamic BuildCheckoutButtonShape(
            IEnumerable<dynamic> productShapes,
            IEnumerable<dynamic> shippingMethodShapes,
            IEnumerable<string> custom) {

            var checkoutSettings = GetSettings();

            return _shapeFactory.PaypalCheckout(
                CartItems: productShapes,
                ShippingMethods: shippingMethodShapes,
                Custom: custom,
                Currency: checkoutSettings.Currency,
                WeightUnit: checkoutSettings.WeightUnit,
                MerchantId: checkoutSettings.MerchantId,
                AnalyticsId: checkoutSettings.AnalyticsId,
                UseSandbox: checkoutSettings.UseSandbox,
                ReturnURL: checkoutSettings.ReturnUrl,
                Business: checkoutSettings.MerchantId);
        }
    }
}

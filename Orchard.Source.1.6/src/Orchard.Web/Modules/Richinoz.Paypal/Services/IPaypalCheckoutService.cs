using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services {
    public interface IPaypalCheckoutService : ICheckoutService
    {
        PaypalCheckoutSettingsPart GetSettings();
    }
}
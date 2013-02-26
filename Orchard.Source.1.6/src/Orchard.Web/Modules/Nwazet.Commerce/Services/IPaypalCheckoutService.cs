using Nwazet.Commerce.Models;

namespace Nwazet.Commerce.Services {
    public interface IPaypalCheckoutService : ICheckoutService
    {
        PaypalCheckoutSettingsPart GetSettings();
    }
}
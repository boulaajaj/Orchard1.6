using Orchard;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services {
    public interface IOrderService:IDependency
    {
        OrderPart Create();
        OrderPart Get(int id);
    }
}
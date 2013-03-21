using Orchard;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Controllers {
    public interface IOrderService:IDependency {
        Order Create();
        Order Get(int id);
    }
}
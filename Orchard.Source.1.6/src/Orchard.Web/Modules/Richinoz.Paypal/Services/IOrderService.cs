using Orchard;
using Orchard.ContentManagement;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services {
    public interface IOrderService:IDependency {
        ContentItem CreateOrder();       
        ContentItem Get(int id);
    }
}
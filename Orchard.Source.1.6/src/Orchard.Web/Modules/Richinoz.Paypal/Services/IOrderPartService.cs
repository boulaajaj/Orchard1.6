using Orchard;
using Orchard.ContentManagement;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services {
    public interface IOrderPartService:IDependency {
        ContentItem CreateOrder();       
        ContentItem Get(int id);
    }
}
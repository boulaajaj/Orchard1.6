using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Handlers {
    public class OrderHandler:ContentHandler {
        public OrderHandler(IRepository<OrderPartRecord> orderRepository)
        {
            Filters.Add(StorageFilter.For(orderRepository));         
        }
    }
}


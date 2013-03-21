using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Services;
using Richinoz.Paypal.Controllers;
using Richinoz.Paypal.Helpers;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services {
    public class OrderService : IOrderService {
        private readonly IOrderPartService _orderPartService;
        private readonly IClock _clock;

        public OrderService(IOrderPartService orderPartService, 
            IClock clock) {
            _orderPartService = orderPartService;
            _clock = clock;
        }

        public IOrder Create() {
            var contentItem = _orderPartService.CreateOrder();
            var orderPart = contentItem.As<OrderPart>(); 
            var order = SerialisationUtils.DeserializeFromXml<Order>(orderPart.Details);
            return order;
        }

        public IOrder Get(int id) {
            var contentItem = _orderPartService.Get(id);
            Order order = null;
            if(contentItem!=null) {
                var orderPart = contentItem.As<OrderPart>();                
                order = SerialisationUtils.DeserializeFromXml<Order>(orderPart.Details);                
            }
            return order;
        }

        public void Save(IOrder order) {
            var contentItem = _orderPartService.Get(order.Id);
            var orderPart = contentItem.As<OrderPart>();

            orderPart.Details = SerialisationUtils.SerializeToXml(order);
            orderPart.TransactionId = order.TransactionId;
            orderPart.Amount = order.AmountPaid;                                    

            var utcNow = _clock.UtcNow;

            if (contentItem.Has<CommonPart>()) {
                contentItem.As<CommonPart>().ModifiedUtc = utcNow;
                contentItem.As<CommonPart>().VersionModifiedUtc = utcNow;
            }
            if (contentItem.Has<TitlePart>()) 
                contentItem.As<TitlePart>().Title = string.Format("{0}_{1}_{2}", order.Address.FirstName, order.Address.LastName, utcNow.ToShortDateString());

        }
    }
}
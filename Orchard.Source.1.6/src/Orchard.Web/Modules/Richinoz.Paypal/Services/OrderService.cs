using System;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Title.Models;
using Orchard.Data;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrchardServices _orchardServices;

        public OrderService(IOrchardServices orchardServices)
        {
            _orchardServices = orchardServices;
        }

        public ContentItem CreateOrder() {
            try
            {
                var order = _orchardServices.ContentManager.New("Order");

                _orchardServices.ContentManager.Create(order, VersionOptions.Draft);

                return order;
            }
            catch (Exception)
            {
                _orchardServices.TransactionManager.Cancel();
                throw;
            }
        }

        //public OrderPart Create()
        //{

        //    try
        //    {
        //        var order = _orchardServices.ContentManager.New("Order");
                
        //        _orchardServices.ContentManager.Create(order, VersionOptions.Draft);

        //        return order.As<OrderPart>();
        //    }
        //    catch (Exception)
        //    {
        //        _orchardServices.TransactionManager.Cancel();
        //        throw;
        //    }
        //}

        public ContentItem Get(int id) {
            return _orchardServices.ContentManager.Get(id, VersionOptions.AllVersions);
        }
    }
}
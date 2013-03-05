using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Drivers {
    public class OrderPartDriver:ContentPartDriver<OrderPart> {

        protected override string Prefix
        {
            get { return "Order"; }
        }

    
    }
}
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

        protected override DriverResult Display(OrderPart part, string displayType, dynamic shapeHelper)
        {
            return Combined(
                ContentShape("Parts_Order", () => shapeHelper.Parts_Order(OrderPart: part)));
        }
    
    }
}
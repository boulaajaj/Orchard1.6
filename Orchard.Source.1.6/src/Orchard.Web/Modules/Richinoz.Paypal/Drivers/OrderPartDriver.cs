using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Data;
using Richinoz.Paypal.Helpers;
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

        //get
        protected override DriverResult Editor(OrderPart part, dynamic shapeHelper)
        {
            var order = SerialisationUtils.DeserializeFromXml<Order>(part.Details);
            part.Order = order;

            return ContentShape("Parts_Order_Edit", () =>
                                                    shapeHelper.EditorTemplate(TemplateName: "Parts/Order", Model: part, Prefix: Prefix));
        }

        //post
        protected override DriverResult Editor(OrderPart part, IUpdateModel updater, dynamic shapeHelper)
        {                      
            if(updater.TryUpdateModel(part, Prefix, null, new string[] { "TransactionId" }))
                part.Details = SerialisationUtils.SerializeToXml(part.Order);

            return Editor(part, shapeHelper);
        }

    }

    
   
}
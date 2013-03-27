using System.Collections.Generic;
using System.Xml.Serialization;
using Orchard;
using Richinoz.Paypal.Models;

namespace Richinoz.Paypal.Services
{
    public interface IOrderService : IDependency
    {
        int Create(IOrder order);
        IOrder Get(int id);
        void Save(IOrder order);
    }

    public interface IOrder
    {
        int UniqueId { get; set; }
        List<IOrderItem> OrderItems { get; set; }
        IAddress Address { get; set; }
        decimal OriginalAmount { get; set; }
        decimal AmountPaid { get; set; }
        string TransactionId { get; set; }
    }

    public interface IAddress
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Street1 { get; set; }
        string City { get; set; }
        string StateOrProvince { get; set; }
        string Country { get; set; }
        string Zip { get; set; }
        string UserName { get; set; }
    }

    public interface IOrderItem
    {
        string Name { get; set; }
        decimal Amount { get; set; }
        int Quantity { get; set; }
    }
}
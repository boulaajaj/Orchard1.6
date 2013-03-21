using System;
using Richinoz.Paypal.Controllers;
using Richinoz.Paypal.Services;

namespace Richinoz.Paypal.Models
{
    [Serializable]
    public class Address:IAddress
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Street1 { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string UserName { get; set; }
    }
}

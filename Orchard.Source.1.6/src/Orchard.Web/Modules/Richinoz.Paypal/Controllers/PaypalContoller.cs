using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Logging;
using Orchard.UI.Admin;
using Richinoz.Paypal.Helpers;
using Richinoz.Paypal.Models;
using Richinoz.Paypal.Services;

namespace Richinoz.Paypal.Controllers
{
    public interface IWebRequestFactory : IDependency
    {
        WebRequest Create(string uri);
    }

    public class WebRequestFactory : IWebRequestFactory
    {
        public WebRequest Create(string uri)
        {
            return (HttpWebRequest)WebRequest.Create(uri);
        }
    }

    public class PaypalController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IWebRequestFactory _webRequestFactory;
        public ILogger Logger { get; set; }
        private const string OrderId = "custom";

        public PaypalController(IOrderService orderService,
            IWebRequestFactory webRequestFactory)
        {
            _orderService = orderService;
            _webRequestFactory = webRequestFactory;
            Logger = NullLogger.Instance;
        }

        //
        // GET: /Paypal/


        public ActionResult Success(Order order)
        {
            return View(order);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult PostToPaypal(string checkout_Url)
        {

            var orderPart = _orderService.CreateOrder();
            var orderId = orderPart.Id;

            var query = Request.Form.ToString();// HttpUtility.ParseQueryString(Request.RawUrl);

            var paypalUrl = string.Format("{0}?{1}&{2}={3}", checkout_Url, query, OrderId, orderId);

            orderPart.As<OrderPart>().Details = SerialiseOrder(orderId);
            orderPart.As<TitlePart>().Title = "UnVerified Order";

            return Redirect(paypalUrl);

        }


        public ActionResult IPN()
        {
            var formVals = new Dictionary<string, string> {
                {"cmd", "_notify-validate"}
            };

            var response = GetPayPalResponse(formVals, true);

            string logNote = string.Format("{0}, {1}", response, Request["txn_id"]);
            //Logger.Error("IPN response: " + logNote);
            if (response == "VERIFIED")
            {

                string transactionID = Request["txn_id"];
                string sAmountPaid = Request["mc_gross1"];
                int orderId;
                if (!int.TryParse(Request[OrderId], out orderId))
                {
                    Logger.Error("No order Id found in Request variable");
                    return null;
                }

                var contentItem = _orderService.Get(orderId);
                if (contentItem == null) {
                    Logger.Error(string.Format("No order found for orderId [{0}]", orderId));
                    return null;
                }

                var orderPart = contentItem.As<OrderPart>();

                //validate the order
                Decimal amountPaid = 0;
                Decimal.TryParse(sAmountPaid, out amountPaid);

                if (AmountPaidIsValid(orderPart, amountPaid))
                {
                    //process itPAY
                    try
                    {
                        var address = new Address
                        {
                            FirstName = Request["first_name"],
                            LastName = Request["last_name"],
                            Email = Request["payer_email"],
                            Street1 = Request["address_street"],
                            City = Request["address_city"],
                            StateOrProvince = Request["address_state"],
                            Country = Request["address_country"],
                            Zip = Request["address_zip"],
                            //UserName = order.UserName
                        };

                       
                        //re-hydrate
                        var order = SerialisationUtils.DeserializeFromXml<Order>(orderPart.Details);
                        order.Address = address;
                        
                        orderPart.Details = SerialisationUtils.SerializeToXml(order);
                        orderPart.TransactionId = transactionID;
                       
                        contentItem.As<TitlePart>().Title = string.Format("{0}_{1}", address.FirstName, address.LastName);
                        contentItem.As<CommonPart>().ModifiedUtc = DateTime.Now;

                        Logger.Information("{0}{1}", "IPN Order successfully transacted:", orderId);

                        return View("Success");
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "Error in Paypal IPN");
                    }
                }

            }

            return null;
        }

        public string SerialiseOrder(int orderId)
        {

            var orderDetails = new Order() { Id = orderId };

            try
            {
                int i = 0;

                while (true)
                {
                    i++;
                    var name = Request.Form[string.Format("item_name_{0}", i)];

                    if (name == null)
                        break;
                    decimal amount = 0;
                    decimal.TryParse(Request.Form[string.Format("amount_{0}", i)], out amount);

                    int qty = 0;
                    int.TryParse(Request.Form[string.Format("quantity_{0}", i)], out qty);

                    orderDetails.OrderItems.Add(new OrderItem()
                    {
                        Amount = amount,
                        Quantity = qty,
                        Name = name
                    });
                }

            }
            catch (Exception ex)
            {
                return "SerialiseOrder Error-" + ex.Message;
            }
            return SerialisationUtils.SerializeToXml(orderDetails);


        }
        /// <summary>
        /// Handles the PDT Response from PayPal
        /// </summary>
        /// <returns></returns>
        //public ActionResult PDT()
        //{
        //    //_logger.Info("PDT Invoked");
        //    string transactionID = Request.QueryString["tx"];
        //    string sAmountPaid = Request.QueryString["amt"];
        //    string orderID = Request.QueryString["cm"];


        //    Dictionary<string, string> formVals = new Dictionary<string, string>();
        //    formVals.Add("cmd", "_notify-synch");
        //    formVals.Add("at", SiteData.PayPalPDTToken);
        //    formVals.Add("tx", transactionID);

        //    string response = GetPayPalResponse(formVals, true);
        //    //_logger.Info("PDT Response received: " + response);
        //    if (response.StartsWith("SUCCESS"))
        //    {
        //        //_logger.Info("PDT Response received for order " + orderID);

        //        //validate the order
        //        Decimal amountPaid = 0;
        //        Decimal.TryParse(sAmountPaid, out amountPaid);


        //        Order order = null;

        //        if (AmountPaidIsValid(order, amountPaid))
        //        {


        //            Address add = new Address();
        //            add.FirstName = GetPDTValue(response, "first_name");
        //            add.LastName = GetPDTValue(response, "last_name");
        //            add.Email = GetPDTValue(response, "payer_email");
        //            add.Street1 = GetPDTValue(response, "address_street");
        //            add.City = GetPDTValue(response, "address_city");
        //            add.StateOrProvince = GetPDTValue(response, "address_state");
        //            add.Country = GetPDTValue(response, "address_country");
        //            add.Zip = GetPDTValue(response, "address_zip");
        //            add.UserName = order.UserName;


        //            //process it
        //            try
        //            {
        //                // _pipeline.AcceptPalPayment(order, transactionID, amountPaid);
        //                // _logger.Info("PDT Order successfully transacted: " + orderID);
        //                return RedirectToAction("Receipt", "Order", new { id = order.ID });
        //            }
        //            catch
        //            {
        //                //HandleProcessingError(order, x);
        //                return View();
        //            }

        //        }
        //        else
        //        {
        //            //Payment Amount is off
        //            //this can happen if you have a Gift cert at PayPal
        //            //be careful of this!
        //            //HandleProcessingError(order, new InvalidOperationException("Amount paid (" + amountPaid.ToString("C") + ") was below the order total"));
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ViewData["message"] = "Your payment was not successful with PayPal";
        //        return View();
        //    }
        //}
        /// <summary>
        /// Utility method for handling PayPal Responses
        /// </summary>
        public string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";


            var req = _webRequestFactory.Create(paypalUrl);// (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            var sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://urlort#");
            //req.Proxy = proxy;
            //Send the request to PayPal and get the response
            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
        bool AmountPaidIsValid(OrderPart order, decimal amountPaid)
        {

            //pull the order
            bool result = true;

            if (order != null)
            {
                if (order.Amount > amountPaid)
                {
                    //_logger.Warn("Invalid order Amount to PDT/IPN: " + order.ID + "; Actual: " + amountPaid.ToString("C") + "; Should be: " + order.Total.ToString("C") + "user IP is " + Request.UserHostAddress);
                    result = false;
                }
            }
            else
            {
                //_logger.Warn("Invalid order ID passed to PDT/IPN; user IP is " + Request.UserHostAddress);
            }
            return result;

        }
        string GetPDTValue(string pdt, string key)
        {

            string[] keys = pdt.Split('\n');
            string thisVal = "";
            string thisKey = "";
            foreach (string s in keys)
            {
                string[] bits = s.Split('=');
                if (bits.Length > 1)
                {
                    thisVal = bits[1];
                    thisKey = bits[0];
                    if (thisKey.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                        break;
                }
            }
            return thisVal;


        }

    }
}

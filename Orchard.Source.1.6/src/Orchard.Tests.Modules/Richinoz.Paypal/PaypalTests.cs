﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Moq;
using NUnit.Framework;
using Orchard.Comments.Handlers;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Services;
using Orchard.Tests.Stubs;
using RichSite.Helpers;
using Richinoz.Paypal.Controllers;
using Richinoz.Paypal.Helpers;
using Richinoz.Paypal.Models;
using Richinoz.Paypal.Services;

namespace Orchard.Tests.Modules.Richinoz.Paypal
{
    [TestFixture]
    public class PaypalTests
    {
        private IContainer _container;
        private IOrderPartService _orderPartService;
        private Mock<IOrderPartService> _orderPartServiceMock;

        public PaypalTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<StubClock>().As<IClock>();
            builder.RegisterType<OrderService>().As<IOrderService>();

            _orderPartServiceMock = new Mock<IOrderPartService>();
            _orderPartService = _orderPartServiceMock.Object;

            builder.RegisterInstance(_orderPartService).As<IOrderPartService>();

            //var assembly = Assembly.GetAssembly(typeof(OrchardServices));

            _container = builder.Build();
        }
        [SetUp]
        public void Init()
        {

        }

        [Test]
        public void can_add_to_orderPart()
        {

            //var OrderPartService = _container.Resolve<IOrderPartService>();
            var orderPartService = new Mock<IOrderPartService>();
            var webRequest = new Mock<IWebRequestFactory>();
            var orderService = new Mock<IOrderService>();

            var paypalController = new PaypalController(orderPartService.Object, orderService.Object, webRequest.Object, _container.Resolve<IClock>());


            var ret = paypalController.SerialiseOrder(1);

            Assert.IsNotNullOrEmpty(ret);

        }

        [Test]
        public void Create_Returns_ViewResult_On_Success()
        {
            var server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);

            var request = new Mock<HttpRequestBase>(MockBehavior.Loose);
            request.Setup(x => x.ApplicationPath).Returns("/");
            request.Setup(x => x.Url).Returns(new Uri("http://localhost"));
            request.Setup(x => x.BinaryRead(It.IsAny<int>())).Returns(new byte[] { });
        
            request.SetupGet(r => r["txn_id"]).Returns("Tx101");
            request.SetupGet(r => r["custom"]).Returns("1");
            request.SetupGet(r => r["mc_gross"]).Returns("1");
            request.SetupGet(r => r.ContentLength).Returns(1);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            context.SetupGet(x => x.Server).Returns(server.Object);

            var yourMockedResponseStream = new MemoryStream();

            var sw = new StreamWriter(yourMockedResponseStream);            
            sw.Write("VERIFIED");
            sw.Flush();
            yourMockedResponseStream.Position = 0;

            var streamReader = new MemoryStream();

            var webResponse = new Mock<WebResponse>();
            webResponse.Setup(c => c.GetResponseStream()).Returns(yourMockedResponseStream);

            var webRequest = new Mock<WebRequest>();
            webRequest.Setup(c => c.GetResponse()).Returns(webResponse.Object);
            webRequest.Setup(c => c.GetRequestStream()).Returns(streamReader);

            var factory = new Mock<IWebRequestFactory>();
            factory.Setup(c => c.Create(It.IsAny<string>())).Returns(webRequest.Object);

            
            var orderService = _container.Resolve<IOrderService>();// new Mock<IOrderService>();

            var orderPart = new Mock<OrderPart>();
            orderPart.Setup(x => x.Amount).Returns(1);
            orderPart.Setup(x => x.Details).Returns(SerialisationUtils.SerializeToXml(new Order()));
            orderPart.Setup(x => x.TransactionId).Returns("testTXN");   

            var contentItem =new ContentItem();            
            contentItem.Weld(orderPart.Object);

            _orderPartServiceMock.Setup(x => x.Get(It.IsAny<int>())).Returns(contentItem);
            var controller = new PaypalController(_orderPartServiceMock.Object, orderService, factory.Object, _container.Resolve<IClock>());
            
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            
            //act
            var results = controller.IPN();

            //assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOf(typeof(ViewResult), results);

            orderPart.VerifySet(x => x.TransactionId = "Tx101");
        }

    }
}
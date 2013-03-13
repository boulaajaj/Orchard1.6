using System;
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
using Orchard.Comments.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Common.Handlers;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.DisplayManagement.Descriptors;
using Orchard.DisplayManagement.Implementation;
using Orchard.Environment;
using Orchard.Environment.Configuration;
using Orchard.Environment.Extensions;
using Orchard.Environment.Extensions.Folders;
using Orchard.Recipes.Services;
using Orchard.Roles.Services;
using Orchard.Security;
using Orchard.Tests.Modules.Comments.Services;
using Orchard.Tests.Stubs;
using Orchard.UI.Notify;
using Orchard.Utility.Extensions;
using Richinoz.Paypal.Controllers;
using Richinoz.Paypal.Models;
using Richinoz.Paypal.Services;

namespace Orchard.Tests.Modules.Richinoz.Paypal
{
    [TestFixture]
    public class PaypalTests
    {
        private IContainer _container;

        public PaypalTests()
        {
            //var builder = new ContainerBuilder();


            //var assembly = Assembly.GetAssembly(typeof(OrchardServices));

            //builder.RegisterAssemblyTypes(assembly);


            //builder.RegisterType<OrderService>().As<IOrderService>();
            //builder.RegisterType<SessionLocator>().As<ISessionLocator>();
            //builder.RegisterType<SessionFactoryHolder>().As<ISessionFactoryHolder>();
            //builder.RegisterType<ShellSettings>().As<ShellSettings>();

            //builder.RegisterType<CommentService>().As<ICommentService>();
            //builder.RegisterType<StubCommentValidator>().As<ICommentValidator>();
            //builder.RegisterType<DefaultContentManager>().As<IContentManager>();
            //builder.RegisterType<DefaultContentManagerSession>().As<IContentManagerSession>();
            //builder.RegisterInstance(new Mock<IContentDefinitionManager>().Object);
            //builder.RegisterInstance(new Mock<ITransactionManager>().Object);
            //builder.RegisterInstance(new Mock<IAuthorizer>().Object);
            //builder.RegisterInstance(new Mock<INotifier>().Object);
            //builder.RegisterInstance(new Mock<IContentDisplay>().Object);
            //builder.RegisterInstance(new Mock<IAuthenticationService>().Object);
            //builder.RegisterType<OrchardServices>().As<IOrchardServices>();
            //builder.RegisterType<DefaultShapeTableManager>().As<IShapeTableManager>();
            //builder.RegisterType<DefaultShapeFactory>().As<IShapeFactory>();
            //builder.RegisterType<StubWorkContextAccessor>().As<IWorkContextAccessor>();
            //builder.RegisterType<CommentedItemHandler>().As<IContentHandler>();
            //builder.RegisterType<DefaultContentQuery>().As<IContentQuery>();
            //builder.RegisterType<CommentPartHandler>().As<IContentHandler>();
            //builder.RegisterType<CommonPartHandler>().As<IContentHandler>();
            //builder.RegisterType<StubExtensionManager>().As<IExtensionManager>();
            //builder.RegisterType<DefaultContentDisplay>().As<IContentDisplay>();
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            //_container = builder.Build();
        }
        [SetUp]
        public void Init()
        {

        }

        [Test]
        public void can_add_to_orderPart()
        {

            //var orderService = _container.Resolve<IOrderService>();
            var orderService = new Mock<IOrderService>();
            var webRequest = new Mock<IWebRequestFactory>();

            var paypalController = new PaypalController(orderService.Object, webRequest.Object);


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
            request.Setup(x => x.BinaryRead(It.IsAny<int>())).Returns(new byte[]{});
            //request.Setup(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection{
            //        { "SERVER_NAME", "localhost" },
            //        { "SCRIPT_NAME", "localhost" },
            //        { "SERVER_PORT", "80" },
            //        { "HTTPS", "www.melaos.com" },
            //        { "REMOTE_ADDR", "127.0.0.1" },
            //        { "REMOTE_HOST", "127.0.0.1" }
            //   });


            request.SetupGet(r => r["txn_id"]).Returns("Tx101");
            request.SetupGet(r => r.ContentLength).Returns(1);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.Response).Returns(response.Object);
            context.SetupGet(x => x.Server).Returns(server.Object);

            //var memory = new Mock<FileStream>();

            var yourMockedResponseStream = new MemoryStream();   
            yourMockedResponseStream.Write( "Verified".ToByteArray(),0, "Verified".ToByteArray().Length);
            
            var streamReader = new MemoryStream();
            streamReader.Write("Verified".ToByteArray(), 0, "Verified".ToByteArray().Length);

            var webResponse = new Mock<WebResponse>();
            webResponse.Setup(c => c.GetResponseStream()).Returns(yourMockedResponseStream);            

            var webRequest = new Mock<WebRequest>();
            webRequest.Setup(c => c.GetResponse()).Returns(webResponse.Object);
            webRequest.Setup(c => c.GetRequestStream()).Returns(streamReader);
           
            var factory = new Mock<IWebRequestFactory>();
            factory.Setup(c => c.Create(It.IsAny<string>())).Returns(webRequest.Object);


            var orderService = new Mock<IOrderService>();

            var controller = new PaypalController(orderService.Object, factory.Object);
            //MvcMockHelpers.SetFakeControllerContext(controller);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var results = controller.IPN();
            Assert.IsNotNull(results);
            Assert.IsInstanceOf(typeof(ViewResult), results);
        }

    }
}
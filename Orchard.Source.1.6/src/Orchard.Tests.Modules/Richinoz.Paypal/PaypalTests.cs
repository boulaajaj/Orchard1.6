using System;
using System.Reflection;
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
using Richinoz.Paypal.Controllers;
using Richinoz.Paypal.Models;
using Richinoz.Paypal.Services;

namespace Orchard.Tests.Modules.Richinoz.Paypal {
    [TestFixture]
    public class PaypalTests {
        private IContainer _container;

        public PaypalTests() {
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
        public void Init() {
         
        }
    
        [Test]
        public void can_add_to_orderPart() {

            //var orderService = _container.Resolve<IOrderService>();
            var orderService = new Mock<IOrderService>();

            var paypalController = new PaypalController(orderService.Object);


            var ret =paypalController.SerialiseOrder(1);

            Assert.IsNotNullOrEmpty(ret);

        }
      
    }
}
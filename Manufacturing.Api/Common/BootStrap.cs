using EventSource.Framework;
using Manufacturing.Common;
using Manufacturing.Domain.Configuration;
using Manufacturing.Domain.Handlers.WorkOrders;
using StructureMap;

namespace Manufacturing.Api.Common
{
    public static class Bootstrap
    {
        public static IContainer Init()
        {
            var container = IoC();

            SetupDatabase(container);

            return container;
        }

        public static void SetupDatabase(IContainer container){
            container.Configure(x=>x.AddRegistry<DomainRegistry>());
        }

        public static IContainer IoC()
        {
            return new Container(x =>
            {
                x.For<IEventPublisher>().Use<DummyPublisher>();
            });
        }
    }
}
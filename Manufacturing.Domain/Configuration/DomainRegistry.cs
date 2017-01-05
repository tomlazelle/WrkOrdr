using System.Collections.Generic;
using EventSource.Framework;
using Manufacturing.Common;
using Manufacturing.Domain.EventStores;
using Manufacturing.Domain.Handlers.WorkOrders;
using Raven.Client;
using Raven.Client.Converters;
using Raven.Client.Document;
using StructureMap;

namespace Manufacturing.Domain.Configuration
{
    public class DomainRegistry:Registry
    {
        public DomainRegistry()
        {
            For<IDocumentStore>().Singleton().Use(x=>CreateNewStore(x));
            ForConcreteType<WorkOrderHandler>();            
            For<IEventStore>().Use<RavenDBEventStore>();
            For<IEventPublisher>().Use<DummyPublisher>();

        }

        private IDocumentStore CreateNewStore(IContext context)
        {
            var store = new DocumentStore
            {
                DefaultDatabase = "Manufacturing",
                Url = context.GetInstance<IConfigMgr>().Get<string>("ManufacturingDb")
            }.Initialize();

            

            store.Conventions.IdentityTypeConvertors = new List<ITypeConverter>
            {
                new GuidConverter()
            };

            //            IndexCreation.CreateIndexes(typeof(Zip).Assembly, _store);
            return store;
        }
    }
}
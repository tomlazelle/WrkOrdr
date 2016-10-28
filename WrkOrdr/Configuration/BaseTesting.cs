using System;
using System.Collections.Generic;
using System.Linq;
using EventSource.Framework;
using Manufacturing.Domain;
using Ploeh.AutoFixture;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Converters;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Database.Linq.PrivateExtensions;
using WrkOrdr.TestObjects;
using WrkOrdr.TestObjects.Events;
using WrkOrdr.TestObjects.Handlers;

namespace WrkOrdr.Configuration
{
    public abstract class BaseTesting<TClassUnderTest> : IBaseTest
      where TClassUnderTest : class
    {
        protected IFixture _fixture;

        private TClassUnderTest _sut;
        protected TClassUnderTest Sut
        {
            get
            {
                return _sut ?? (_sut = new Lazy<TClassUnderTest>(() => _fixture.Create<TClassUnderTest>()).Value);
            }
        }


        public virtual void FixtureSetup(IFixture fixture)
        {
            _fixture = fixture;
        }

        public virtual void FixtureTearDown()
        {
        }

        protected void Register<TInterface>(TInterface concreteType)
        {
            _fixture.Register(() => concreteType);
        }

        protected T MockType<T>()
        {
            return _fixture.Create<T>();
        }

        public void RegisterDatabase()
        {
            var _store = new DocumentStore
            {
                                                Url = "http://localhost:8080/", // server URL
                                                DefaultDatabase = "EventSource",
//                RunInMemory = true,

            };

//            _store.Configuration.Storage.Voron.AllowOn32Bits = true;

            _store.Initialize();

            _store.Conventions.IdentityTypeConvertors = new List<ITypeConverter>
            {
                new GuidConverter()
            };

//            IndexCreation.CreateIndexes(typeof(Zip).Assembly, _store);

            _fixture.Register<IDocumentStore>(() => _store);
            _fixture.Register<IEventStore>(() => new EventStore(_store));
        }
    }


    public class Zip : AbstractIndexCreationTask<WorkOrderEvents, CreateWorkOrderItemEvent>
    {
        public Zip()
        {
            Map = wo => from parent in wo
                        select new EventTypeResult
                        {
                            Id = parent.Id,
                            OrderId = ((CreateWorkOrderEvent)parent.Events.FirstOrDefault(x => x.EventType == "CreateWorkOrderEvent")).OrderId,
                            OrderItemId = ((CreateWorkOrderEvent)parent.Events.FirstOrDefault(x => x.EventType == "CreateWorkOrderEvent")).OrderItemId,
                            Status = ((CreateWorkOrderEvent)parent.Events.FirstOrDefault(x => x.EventType == "CreateWorkOrderEvent")).Status
                        };
        }

        public class EventTypeResult
        {
            public Guid Id { get; set; }
            public int OrderId { get; set; }
            public int OrderItemId { get; set; }
            public WorkOrderStatus Status { get; set; }
        }
    }
}
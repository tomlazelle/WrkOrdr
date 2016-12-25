using System;
using System.Collections.Generic;
using EventSource.Framework;
using Manufacturing.Domain;
using Ploeh.AutoFixture;
using Raven.Client;
using Raven.Client.Converters;
using Raven.Client.Document;

namespace WrkOrdr.Tests.Configuration
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


    
}
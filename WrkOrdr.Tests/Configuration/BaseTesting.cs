﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using EventSource.Framework;
using Manufacturing.Common;
using Manufacturing.Domain;
using NSubstitute;
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
            var configMgr = Substitute.For<IConfigMgr>();
            configMgr.Get<string>("ManufacturingDb").Returns("http://localhost:8080");
            Register(configMgr);
        }

        public virtual void FixtureTearDown()
        {
        }

        protected void Register<TInterface>(TInterface concreteType)
        {
            _fixture.Register<TInterface>(() => concreteType);
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
           

            Register(new TestActivator());
            Register<IDocumentStore>(_store);
            Register<IEventStore>(new EventStore(_store, new TestActivator()));
        }
    }

    public class TestActivator:ITypeActivator
    {
        public T Instance<T>()
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }


}
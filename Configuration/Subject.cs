using System;
using Ploeh.AutoFixture;

namespace WrkOrdr.Configuration
{
    public abstract class Subject<TClassUnderTest> : IBaseTest where TClassUnderTest : class
    {
        protected IFixture _fixture;

        private TClassUnderTest _sut;
        protected TClassUnderTest Sut
        {
            get { return _sut ?? (_sut = new Lazy<TClassUnderTest>(() => _fixture.Create<TClassUnderTest>()).Value); }
        }


        protected void Register<TInterface>(TInterface concreteType)
        {
            _fixture.Register(() => concreteType);
        }

        protected T MockType<T>()
        {
            return _fixture.Create<T>();
        }

        public virtual void FixtureSetup(IFixture fixture)
        {
            _fixture = fixture;
        }

        public virtual void FixtureTearDown()
        {

        }


    }
}
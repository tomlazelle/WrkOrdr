using Ploeh.AutoFixture;

namespace WrkOrdr.Tests.Configuration
{
    public interface IBaseTest
    {
        void FixtureSetup(IFixture fixture);
        void FixtureTearDown();
    }
}
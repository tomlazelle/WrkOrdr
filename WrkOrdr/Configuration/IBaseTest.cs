using Ploeh.AutoFixture;

namespace WrkOrdr.Configuration
{
    public interface IBaseTest
    {
        void FixtureSetup(IFixture fixture);
        void FixtureTearDown();
    }
}
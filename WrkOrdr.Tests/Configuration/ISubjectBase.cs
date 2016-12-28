using Ploeh.AutoFixture;

namespace WrkOrdr.Tests.Configuration
{
    public interface ISubjectBase
    {
        void FixtureSetup(IFixture fixture);
        void FixtureTearDown();
    }
}
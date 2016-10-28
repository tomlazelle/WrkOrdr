using System.Linq;
using Fixie;

namespace WrkOrdr.Configuration
{
    public class TestConvention : Convention
    {
        public TestConvention()
        {
            FixtureExecution.Wrap<FixtureSetupBehavior>();

            Classes
                .NameEndsWith("test", "Test");

            ClassExecution
                .CreateInstancePerClass();


            Methods
                .Where(
                    x => x.IsVoid() &&
                         x.IsPublic &&
                         x.CustomAttributes.All(a => a.AttributeType.Name != "IgnoreAttribute") &&
                         x.Name != "FixtureSetup" &&
                         x.Name != "FixtureTearDown" &&
                         x.Name != "RegisterDatabase");
        }
    }
}

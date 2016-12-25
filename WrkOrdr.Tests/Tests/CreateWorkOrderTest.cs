using Manufacturing.Domain.Aggregates;
using Manufacturing.Domain.Handlers.WorkOrders;
using Manufacturing.Domain.Messages.WorkOrders;
using Ploeh.AutoFixture;
using Raven.Client;
using Should;
using WrkOrdr.Tests.Configuration;

namespace WrkOrdr.Tests.Tests
{
    public class CreateWorkOrderTest : BaseTesting<WorkOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);

            RegisterDatabase();
        }

        
        public void can_create_a_work_order()
        {
            var createMessage = _fixture.Build<CreateWorkOrderMessage>()
                .With(x => x.Status, WorkOrderStatus.NotStarted)
                .Create();

            var workOrder = Sut.Handle(createMessage);

            workOrder.Id.ShouldEqual(createMessage.Id);
            workOrder.CreateDate.ShouldEqual(createMessage.CreateDate);

            using (var session = _fixture.Create<IDocumentStore>().OpenSession())
            {
                var foundEvent = session.Load<WorkOrderEvents>("WorkOrderEvents/" + createMessage.Id);

                foundEvent.ShouldNotBeNull();
                foundEvent.Events.Length.ShouldEqual(1);
            }
        }
    }
}
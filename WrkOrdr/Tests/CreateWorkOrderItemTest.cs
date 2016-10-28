using System.Collections.Generic;
using System.Linq;
using Manufacturing.Domain;
using Manufacturing.Domain.Handlers;
using Manufacturing.Domain.Messages;
using Ploeh.AutoFixture;
using Raven.Client;
using Should;
using WrkOrdr.Configuration;

namespace WrkOrdr.Tests
{
    public class CreateWorkOrderItemTest : BaseTesting<WorkOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);

            RegisterDatabase();
        }

        public void can_create_a_work_order_item()
        {
            var createMessage = _fixture.Build<CreateWorkOrderMessage>()
                .With(x => x.Status, WorkOrderStatus.NotStarted)
                .Create();

            Sut.Handle(createMessage);

            var itemMessage = _fixture
                .Build<CreateWorkOrderItemMessage>()
                .With(x => x.Id, createMessage.Id)
                .With(x => x.Status, WorkItemStatus.New)
                .With(x => x.Details, _fixture.CreateMany<KeyValuePair<string, object>>(10).ToDictionary(k => k.Key, v => v.Value))
                .Create();


            var workOrder = Sut.Handle(itemMessage);

            workOrder.Items.FirstOrDefault().Details.Count.ShouldEqual(itemMessage.Details.Count);
            workOrder.Version.ShouldEqual(2);

            using (var session = _fixture.Create<IDocumentStore>().OpenSession())
            {
                var foundEvent = session.Load<WorkOrderEvents>("WorkOrderEvents/" + createMessage.Id);

                foundEvent.ShouldNotBeNull();
                foundEvent.Events.Length.ShouldEqual(2);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Manufacturing.Common;
using Manufacturing.Domain.Handlers.WorkOrders;
using Manufacturing.Domain.Messages.WorkOrders;
using Ploeh.AutoFixture;
using Should;
using WrkOrdr.Tests.Configuration;

namespace WrkOrdr.Tests.Tests
{
    public class UpdateWorkOrderStatusTest:Subject<WorkOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);
            RegisterDatabase();
        }

        public void can_change_status_to_canceled(){
            var createMessage = _fixture.Build<CreateWorkOrderMessage>()
               .With(x => x.Status, WorkOrderStatus.NotStarted)
               .Create();

            Sut.Handle(createMessage);

            var itemMessage = _fixture
                .Build<CreateWorkOrderItemMessage>()
                .With(x => x.Id, createMessage.Id)
                .With(x => x.Status, WorkItemStatus.NotStarted)
                .With(x => x.Details, _fixture.CreateMany<KeyValuePair<string, object>>(10).ToDictionary(k => k.Key, v => v.Value))
                .Create();

            //add work order item
            Sut.Handle(itemMessage);

            var workOrder = Sut.Handle(new UpdateWorkOrderStatusMessage
            {
                Id = createMessage.Id,
                Status = WorkOrderStatus.Canceled
            });

            workOrder.Status.ShouldEqual(WorkOrderStatus.Canceled);
            workOrder.Items.First().Status.ShouldEqual(WorkItemStatus.Canceled);
        }
    }
}
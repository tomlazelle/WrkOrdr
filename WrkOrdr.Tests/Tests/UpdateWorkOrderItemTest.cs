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
    public class UpdateWorkOrderItemTest:Subject<WorkOrderHandler>
    {
        public override void FixtureSetup(IFixture fixture)
        {
            base.FixtureSetup(fixture);

            RegisterDatabase();
        }

        public void can_update_work_order_item(){
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

            var workOrder = _fixture.Create<WorkOrderQueryHandler>().Get(createMessage.Id);

            var testItem = workOrder.Items.First();

            var updated = Sut.Handle(new UpdateWorkOrderItemMessage
            {
                Id = workOrder.Id,
                CompleteDate = testItem.CompleteDate.Value.AddDays(1),
                Details = testItem.Details,
                ItemId = testItem.Id,
                Sku = "123456789",
                StartDate = testItem.StartDate
            });

            updated.Items.First().CompleteDate.ShouldEqual(testItem.CompleteDate.Value.AddDays(1));
            updated.Items.First().Sku.ShouldEqual("123456789");
        }
    }
}
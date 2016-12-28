using System;
using System.Linq;
using Manufacturing.Common;
using Manufacturing.Domain.Aggregates;
using Manufacturing.Domain.Events;
using Manufacturing.Domain.Events.WorkOrders;
using Manufacturing.Domain.Handlers;
using Manufacturing.Domain.Handlers.WorkOrders;
using Raven.Client.Indexes;

namespace Manufacturing.Domain.Indexes
{
    public class WorkOrderIdIndex : AbstractIndexCreationTask<WorkOrderEvents, CreateWorkOrderItemEvent>
    {
        public WorkOrderIdIndex()
        {
            Map = wo => from parent in wo
                        select new EventTypeResult
                        {
                            Id = parent.Id,
                            OrderId = ((CreateWorkOrderEvent)parent.Events.FirstOrDefault(x => x.EventType == "CreateWorkOrderEvent")).OrderId,
                            OrderItemId = ((CreateWorkOrderEvent)parent.Events.FirstOrDefault(x => x.EventType == "CreateWorkOrderEvent")).OrderItemId,
                            Status = ((CreateWorkOrderEvent)parent.Events.FirstOrDefault(x => x.EventType == "CreateWorkOrderEvent")).Status
                        };
        }

        public class EventTypeResult
        {
            public Guid Id { get; set; }
            public int OrderId { get; set; }
            public int OrderItemId { get; set; }
            public WorkOrderStatus Status { get; set; }
        }
    }
}
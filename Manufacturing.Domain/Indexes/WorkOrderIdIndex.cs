using System;
using System.Linq;
using Manufacturing.Common;
using Manufacturing.Domain.Events.WorkOrders;
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
                    Status = ((CreateWorkOrderEvent) parent.Events.FirstOrDefault(x => x.EventType == "CreateWorkOrderEvent")).Status
                };
        }

        public class EventTypeResult
        {
            public Guid Id { get; set; }
            public WorkOrderStatus Status { get; set; }
        }
    }
}
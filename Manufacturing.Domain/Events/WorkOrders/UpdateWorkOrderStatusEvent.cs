using System;
using EventSource.Framework;
using Manufacturing.Common;

namespace Manufacturing.Domain.Events.WorkOrders
{
    public class UpdateWorkOrderStatusEvent:VersionedEvent<Guid>
    {
        public UpdateWorkOrderStatusEvent(Guid id, WorkOrderStatus status)
        {
            SourceId = id;
            Status = status;
        }
        public WorkOrderStatus Status { get;  }
    }
}
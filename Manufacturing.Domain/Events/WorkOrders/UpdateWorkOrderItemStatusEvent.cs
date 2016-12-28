using System;
using EventSource.Framework;
using Manufacturing.Common;

namespace Manufacturing.Domain.Events.WorkOrders
{
    public class UpdateWorkOrderItemStatusEvent:VersionedEvent<Guid>
    {
        public UpdateWorkOrderItemStatusEvent(Guid id,int itemId,WorkItemStatus status)
        {
            SourceId = id;
            ItemId = itemId;
            Status = status;
        }
        public WorkItemStatus Status { get;  }
        public int ItemId { get; }
    }
}
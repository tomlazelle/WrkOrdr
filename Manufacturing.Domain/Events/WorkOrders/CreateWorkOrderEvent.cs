using System;
using EventSource.Framework;
using Manufacturing.Common;
using Manufacturing.Domain.Aggregates;

namespace Manufacturing.Domain.Events.WorkOrders
{
    public class CreateWorkOrderEvent : VersionedEvent<Guid>
    {
        public CreateWorkOrderEvent(Guid id, DateTime createDate, DateTime? startDate, DateTime? completeDate, int orderId, int orderItemId, WorkOrderStatus status)
        {
            SourceId = id;
            CreateDate = createDate;
            StartDate = startDate;
            CompleteDate = completeDate;
            OrderId = orderId;
            OrderItemId = orderItemId;
            Status = status;
            EventType = GetType().Name;
        }

        public DateTime CreateDate { get; }
        public DateTime? StartDate { get; }
        public DateTime? CompleteDate { get; }
        public int OrderId { get; set; }
        public int OrderItemId { get; }
        public WorkOrderStatus Status { get; set; }
    }
}
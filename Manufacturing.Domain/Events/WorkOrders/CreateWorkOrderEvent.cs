using System;
using EventSource.Framework;
using Manufacturing.Common;

namespace Manufacturing.Domain.Events.WorkOrders
{
    public class CreateWorkOrderEvent : VersionedEvent<Guid>
    {
        public CreateWorkOrderEvent(Guid id, DateTime createDate, DateTime? startDate, DateTime? completeDate, WorkOrderStatus status)
        {
            SourceId = id;
            CreateDate = createDate;
            StartDate = startDate;
            CompleteDate = completeDate;
            Status = status;
            EventType = GetType().Name;
        }

        public DateTime CreateDate { get; }
        public DateTime? StartDate { get; }
        public DateTime? CompleteDate { get; }
        public WorkOrderStatus Status { get; set; }
    }
}
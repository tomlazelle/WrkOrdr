using System;
using System.Collections.Generic;
using EventSource.Framework;
using Manufacturing.Common;

namespace Manufacturing.Domain.Events.WorkOrders
{
    public class CreateWorkOrderItemEvent : VersionedEvent<Guid>
    {
        public CreateWorkOrderItemEvent(Guid id, string sku, DateTime? startDate, DateTime? completeDate, WorkItemStatus status, IDictionary<string, object> details)
        {
            SourceId = id;
            Sku = sku;
            StartDate = startDate;
            CompleteDate = completeDate;
            Status = status;
            Details = details;
            EventType = GetType().Name;
        }

        public string Sku { get; }
        public DateTime? StartDate { get; }
        public DateTime? CompleteDate { get; }
        public WorkItemStatus Status { get; }
        public IDictionary<string, object> Details { get; }
    }
}
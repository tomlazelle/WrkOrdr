using System;
using System.Collections.Generic;
using EventSource.Framework;

namespace Manufacturing.Domain.Events.WorkOrders
{
    public class UpdateWorkOrderItemEvent : VersionedEvent<Guid>
    {
        public UpdateWorkOrderItemEvent(Guid id, int itemId, string sku, DateTime? startDate, DateTime? completeDate, IDictionary<string, object> details)
        {
            SourceId = id;
            ItemId = itemId;
            Sku = sku;
            StartDate = startDate;
            CompleteDate = completeDate;
            Details = details;
        }

        public int ItemId { get; }
        public string Sku { get; }
        public DateTime? StartDate { get; }
        public DateTime? CompleteDate { get; }
        public IDictionary<string, object> Details { get; }
    }
}
using System;
using System.Collections.Generic;
using Manufacturing.Common;

namespace Manufacturing.Domain.Aggregates
{
    public class WorkOrderItem
    {
        public WorkOrderItem(int id, string sku, DateTime? startDate, DateTime? completeDate, WorkItemStatus status, IDictionary<string, object> details)
        {
            Id = id;
            Sku = sku;
            StartDate = startDate;
            CompleteDate = completeDate;
            Status = status;
            Details = details;
        }

        public int Id { get; }
        public string Sku { get; }
        public DateTime? StartDate { get; }
        public DateTime? CompleteDate { get; }
        public WorkItemStatus Status { get; }
        public IDictionary<string, object> Details { get; }
    }


}
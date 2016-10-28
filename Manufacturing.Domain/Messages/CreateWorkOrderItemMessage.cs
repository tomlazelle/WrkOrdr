using System;
using System.Collections.Generic;

namespace Manufacturing.Domain.Messages
{
    public class CreateWorkOrderItemMessage
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public WorkItemStatus Status { get; set; }
        public IDictionary<string, object> Details { get; set; }
    }
}
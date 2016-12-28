using System;
using System.Collections.Generic;

namespace Manufacturing.Domain.Messages.WorkOrders
{
    public class UpdateWorkOrderItemMessage
    {
        public Guid Id { get; set; }
        public int ItemId { get; set; }
        public string Sku { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public IDictionary<string, object> Details { get; set; }
    }
}
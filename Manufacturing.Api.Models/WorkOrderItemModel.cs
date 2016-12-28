using System;
using System.Collections.Generic;
using Manufacturing.Api.Models.Handlers;
using Manufacturing.Common;

namespace Manufacturing.Api.Models
{
    public class WorkOrderItemModel : Resource
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public WorkItemStatus Status { get; set; }
        public IDictionary<string, object> Details { get; set; }
    }
}
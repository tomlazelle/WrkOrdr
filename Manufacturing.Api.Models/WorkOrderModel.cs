using System;
using System.Collections.Generic;
using Manufacturing.Api.Models.Handlers;
using Manufacturing.Common;

namespace Manufacturing.Api.Models
{
    public class WorkOrderModel : Resource
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public WorkOrderStatus Status { get; set; }
        public IEnumerable<WorkOrderItemModel> Items { get; set; }
        public int Version { get; set; }
    }
}
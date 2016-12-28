using System;
using Manufacturing.Common;

namespace Manufacturing.Domain.Messages.WorkOrders
{
    public class UpdateWorkOrderStatusMessage
    {
        public Guid Id { get; set; }
        public WorkOrderStatus Status { get; set; }
    }
}
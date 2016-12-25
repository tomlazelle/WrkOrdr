using System;
using Manufacturing.Domain.Aggregates;

namespace Manufacturing.Domain.Messages.WorkOrders
{
    public class CreateWorkOrderMessage
    {
        public CreateWorkOrderMessage()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public WorkOrderStatus Status { get; set; }
    }
}
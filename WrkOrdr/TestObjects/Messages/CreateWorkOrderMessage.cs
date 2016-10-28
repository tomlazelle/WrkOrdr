using System;

namespace WrkOrdr.TestObjects.Messages
{
    public class CreateWorkOrderMessage
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public WorkOrderStatus Status { get; set; }
    }
}
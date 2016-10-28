using System;
using System.Collections.Generic;
using EventSource.Framework;
using Manufacturing.Domain.Events;
using Manufacturing.Domain.Handlers;

namespace Manufacturing.Domain
{
    public class WorkOrder : BaseEntity<Guid>
    {
        private List<WorkOrderItem> _items = new List<WorkOrderItem>();

        public WorkOrder(Guid id) : base(id)
        {
            Handles<CreateWorkOrderEvent>(NewWorkOrder);
            Handles<CreateWorkOrderItemEvent>(AddWorkOrderItem);
        }

        public WorkOrder(Guid id, WorkOrderEvents eventItems) : this(id)
        {
            LoadEvents(eventItems.Events);
        }


        public int OrderId { get; private set; }
        public int OrderItemId { get; private set; }

        public DateTime CreateDate { get; private set; }
        public DateTime? StartDate { get; private set; }

        public DateTime? CompleteDate { get; private set; }

        public WorkOrderStatus Status { get; private set; }

        public IEnumerable<WorkOrderItem> Items
        {
            get
            {
                if (_items == null) return _items = new List<WorkOrderItem>();

                return _items.AsReadOnly();
            }
        }

        public int Version { get; private set; }

        private void LoadEvents(IEnumerable<IVersionedEvent<Guid>> pastEvents)
        {
            foreach (var e in pastEvents)
            {
                _handlers[e.GetType()].Invoke(e);
                Version = e.Version;
            }
        }


        private void NewWorkOrder(CreateWorkOrderEvent createWorkOrderEvent)
        {
            CreateDate = createWorkOrderEvent.CreateDate;
            OrderItemId = createWorkOrderEvent.OrderItemId;
            OrderId = createWorkOrderEvent.OrderId;
            OrderItemId = createWorkOrderEvent.OrderItemId;
            StartDate = createWorkOrderEvent.StartDate;
            CompleteDate = createWorkOrderEvent.CompleteDate;
            Status = createWorkOrderEvent.Status;
        }

        private void AddWorkOrderItem(CreateWorkOrderItemEvent createWorkOrderItemEvent)
        {
            _items.Add(new WorkOrderItem(_items.Count + 1, createWorkOrderItemEvent.Sku, createWorkOrderItemEvent.StartDate, createWorkOrderItemEvent.CompleteDate, createWorkOrderItemEvent.Status, createWorkOrderItemEvent.Details));
        }
    }
}
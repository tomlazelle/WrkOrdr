using System;
using System.Collections.Generic;
using System.Linq;
using EventSource.Framework;
using Manufacturing.Common;
using Manufacturing.Domain.Events.WorkOrders;
using Manufacturing.Domain.Handlers.WorkOrders;

namespace Manufacturing.Domain.Aggregates
{
    public class WorkOrder : BaseEntity<Guid>
    {
        private List<WorkOrderItem> _items = new List<WorkOrderItem>();

        public WorkOrder(Guid id) : base(id)
        {
            Handles<CreateWorkOrderEvent>(NewWorkOrder);
            Handles<CreateWorkOrderItemEvent>(AddWorkOrderItem);
            Handles<UpdateWorkOrderStatusEvent>(StatusChanged);
            Handles<UpdateWorkOrderItemStatusEvent>(ItemStatusChanged);
            Handles<UpdateWorkOrderItemEvent>(ItemChanged);
        }

        private void ItemChanged(UpdateWorkOrderItemEvent updateWorkOrderItem)
        {
            var item = _items.FirstOrDefault(x => x.Id == updateWorkOrderItem.ItemId);

            _items.Remove(item);

            _items.Add(new WorkOrderItem(item.Id, updateWorkOrderItem.Sku, updateWorkOrderItem.StartDate, updateWorkOrderItem.CompleteDate, item.Status, updateWorkOrderItem.Details));
        }

        private void ItemStatusChanged(UpdateWorkOrderItemStatusEvent updateWorkOrderItemStatusEvent)
        {
            var item = _items.FirstOrDefault(x => x.Id == updateWorkOrderItemStatusEvent.ItemId);

            _items.Remove(item);

            _items.Add(new WorkOrderItem(item.Id, item.Sku, item.StartDate, item.CompleteDate, updateWorkOrderItemStatusEvent.Status, item.Details));
        }

        private void StatusChanged(UpdateWorkOrderStatusEvent updateWorkOrderStatusEvent)
        {
            Status = updateWorkOrderStatusEvent.Status;
        }

        public WorkOrder(Guid id, WorkOrderEvents eventItems) : this(id)
        {
            LoadEvents(eventItems.Events);
        }


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

       


        private void NewWorkOrder(CreateWorkOrderEvent createWorkOrderEvent)
        {
            CreateDate = createWorkOrderEvent.CreateDate;
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
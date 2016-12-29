using System;
using EventSource.Framework;
using Manufacturing.Common;
using Manufacturing.Domain.Aggregates;
using Manufacturing.Domain.Events.WorkOrders;
using Manufacturing.Domain.Messages.WorkOrders;

namespace Manufacturing.Domain.Handlers.WorkOrders
{
    public class WorkOrderHandler :
        IMessageHandler<CreateWorkOrderMessage, WorkOrder>,
        IMessageHandler<CreateWorkOrderItemMessage, WorkOrder>,
        IMessageHandler<UpdateWorkOrderStatusMessage, WorkOrder>,
        IMessageHandler<UpdateWorkOrderItemMessage, WorkOrder>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IEventStore _eventStore;

        public WorkOrderHandler(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public WorkOrder Handle(CreateWorkOrderItemMessage message)
        {
            var workOrderItemEvent = new CreateWorkOrderItemEvent(message.Id, message.Sku, message.StartDate, message.CompleteDate, message.Status, message.Details);

            var workOrderEvents = _eventStore.AddEvent<WorkOrderEvents>(message.Id, workOrderItemEvent);

            _eventPublisher.Publish(message);

            return new WorkOrder(message.Id, workOrderEvents);
        }


        public WorkOrder Handle(CreateWorkOrderMessage message)
        {
            var createWorkOrderEvent = new CreateWorkOrderEvent(
                message.Id, 
                message.CreateDate, 
                message.StartDate, 
                message.CompleteDate, 
                message.Status);

            var workOrderEvents = _eventStore.AddEvent<WorkOrderEvents>(message.Id, createWorkOrderEvent);

            //this is an over simplification of sending a message
            _eventPublisher.Publish(message);

            return new WorkOrder(message.Id, workOrderEvents);
        }

        public WorkOrder Handle(UpdateWorkOrderStatusMessage message)
        {
            var updateWorkOrderEvent = new UpdateWorkOrderStatusEvent(message.Id, message.Status);

            var events = _eventStore.AddEvent<WorkOrderEvents>(message.Id, updateWorkOrderEvent);

            var workOrder = new WorkOrder(message.Id, events);

            if (message.Status == WorkOrderStatus.Canceled)
            {
                foreach (var workOrderItem in workOrder.Items)
                {
                    events = _eventStore.AddEvent<WorkOrderEvents>(message.Id, new UpdateWorkOrderItemStatusEvent(message.Id, workOrderItem.Id, WorkItemStatus.Canceled));
                }
            }

            workOrder = new WorkOrder(message.Id, events);

            //this is an over simplification of sending a message
            _eventPublisher.Publish(message);

            return workOrder;
        }

        public WorkOrder Handle(UpdateWorkOrderItemMessage message)
        {
            var updateWorkOrderItem = new UpdateWorkOrderItemEvent(message.Id, message.ItemId, message.Sku, message.StartDate, message.CompleteDate, message.Details);

            var events = _eventStore.AddEvent<WorkOrderEvents>(message.Id, updateWorkOrderItem);

            //this is an over simplification of sending a message
            _eventPublisher.Publish(message);

            return new WorkOrder(message.Id,events);
        }
    }
}
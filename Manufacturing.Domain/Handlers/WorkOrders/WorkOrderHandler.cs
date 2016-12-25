﻿using EventSource.Framework;
using Manufacturing.Domain.Aggregates;
using Manufacturing.Domain.Events.WorkOrders;
using Manufacturing.Domain.Messages.WorkOrders;

namespace Manufacturing.Domain.Handlers.WorkOrders
{
    public class WorkOrderHandler :
        IMessageHandler<CreateWorkOrderMessage, WorkOrder>,
        IMessageHandler<CreateWorkOrderItemMessage, WorkOrder>
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
            var createWorkOrderEvent = new CreateWorkOrderEvent(message.Id, message.CreateDate, message.StartDate, message.CompleteDate, message.OrderId, message.OrderItemId, message.Status);

            var workOrderEvents = _eventStore.AddEvent<WorkOrderEvents>(message.Id, createWorkOrderEvent);

            //this is an over simplification of sending a message
            _eventPublisher.Publish(message);

            return new WorkOrder(message.Id, workOrderEvents);
        }
    }
}
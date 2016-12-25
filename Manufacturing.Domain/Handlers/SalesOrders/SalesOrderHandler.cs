using EventSource.Framework;
using Manufacturing.Domain.Aggregates;
using Manufacturing.Domain.Events.SalesOrders;
using Manufacturing.Domain.Messages.SalesOrders;

namespace Manufacturing.Domain.Handlers.SalesOrders
{
    public class SalesOrderHandler:IMessageHandler<CreateSalesOrderMessage,SalesOrder>
    {

        private readonly IEventPublisher _eventPublisher;
        private readonly IEventStore _eventStore;

        public SalesOrderHandler(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public SalesOrder Handle(CreateSalesOrderMessage message)
        {
            var salesOrderEvent = new CreateSalesOrderEvent();

            return null;
        }
    }
}
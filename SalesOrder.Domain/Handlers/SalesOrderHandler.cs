using System.Collections.Generic;
using System.Linq;
using SystemNotifications;
using AutoMapper;
using EventSource.Framework;
using Sales.Domain.Aggregates;
using Sales.Domain.Events;
using Sales.Domain.Messages;

namespace Sales.Domain.Handlers
{
    public class SalesOrderHandler : IMessageHandler<CreateSalesOrderMessage, SalesOrder>
    {
        private IEventStore _eventStore;
        private IEventPublisher _eventPublisher;
        
        public SalesOrderHandler(
            IEventStore eventStore,
            IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
            
        }

        public SalesOrder Handle(CreateSalesOrderMessage message)
        {
            var salesOrderCreatedEvent = new SalesOrderCreatedEvent(
                message.Id,
                message.AccountId,
                new Address(
                    message.ShippingAddress.Address1,
                    message.ShippingAddress.Address2,
                    message.ShippingAddress.Address3,
                    message.ShippingAddress.City,
                    message.ShippingAddress.State,
                    message.ShippingAddress.PostalCode),
                new Address(
                    message.BillingAddress.Address1,
                    message.BillingAddress.Address2,
                    message.BillingAddress.Address3,
                    message.BillingAddress.City,
                    message.BillingAddress.State,
                    message.BillingAddress.PostalCode),
                new Person(
                    message.Customer.FirstName,
                    message.Customer.LastName,
                    message.Customer.Phone,
                    message.Customer.Email),
                message.OrderDate,
                message.SubTotal,
                message.Tax,
                message.Total,
                message.DollarsOff,
                message.DiscountPercent,
                message.Items.Select(x => 
                new CreateOrderItemEvent(
                    x.Sku,
                    x.Quantity,
                    x.WholeSalePrice,
                    x.RetailPrice,
                    x.DollarsOff,
                    x.DiscountPercent,
                    x.Details)).ToList());

            var events = _eventStore.AddEvent<SalesOrderEvents>(message.Id, salesOrderCreatedEvent);

            _eventPublisher.Publish(new SalesOrderCreated());

            return new SalesOrder(message.Id, events);
        }

    }
}
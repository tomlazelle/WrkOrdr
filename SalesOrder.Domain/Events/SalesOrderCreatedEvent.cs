using System;
using System.Collections.Generic;
using EventSource.Framework;
using Sales.Domain.Aggregates;

namespace Sales.Domain.Events
{
    public class SalesOrderCreatedEvent : VersionedEvent<Guid>
    {
        public SalesOrderCreatedEvent(Guid id,Guid accountId, Address shippingAddress, Address billingAddress, 
            Person customer,
            Payment paymentData, DateTime orderDate, decimal subTotal, decimal tax, decimal total, decimal dollarsOff, decimal discountPercent, IList<CreateOrderItemEvent> items)
        {
            SourceId = id;
            AccountId = accountId;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Customer = customer;
            PaymentData = paymentData;
            OrderDate = orderDate;
            SubTotal = subTotal;
            Tax = tax;
            Total = total;
            DollarsOff = dollarsOff;
            DiscountPercent = discountPercent;
            Items = items;
        }
        public Guid AccountId { get; }
        public Address ShippingAddress { get; }
        public Address BillingAddress { get; }
        public Person Customer { get; }
        public Payment PaymentData { get; }
        public DateTime OrderDate { get; }
        public decimal SubTotal { get; }
        public decimal Tax { get; }
        public decimal Total { get; }
        public decimal DollarsOff { get; }
        public decimal DiscountPercent { get; }
        public IList<CreateOrderItemEvent> Items { get; }

    }
}
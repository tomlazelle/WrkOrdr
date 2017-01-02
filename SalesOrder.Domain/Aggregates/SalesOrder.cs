using System;
using System.Collections.Generic;
using AutoMapper;
using EventSource.Framework;
using Sales.Common;
using Sales.Domain.Events;
using Sales.Domain.Handlers;

namespace Sales.Domain.Aggregates
{
    public class SalesOrder : BaseEntity<Guid>
    {
        public SalesOrder(Guid id) : base(id)
        {
            Handles<SalesOrderCreatedEvent>(NewSalesOrder);
            Handles<UpdateSalesOrderStatusEvent>(StatusChanged);
        }

        private void StatusChanged(UpdateSalesOrderStatusEvent updateSalesOrderStatusEvent)
        {
            Status = updateSalesOrderStatusEvent.Status;
        }

        private void NewSalesOrder(SalesOrderCreatedEvent salesOrderCreatedEvent)
        {
            AccountId = salesOrderCreatedEvent.AccountId;
            ShippingAddress = salesOrderCreatedEvent.ShippingAddress;
            BillingAddress = salesOrderCreatedEvent.BillingAddress;
            Customer = salesOrderCreatedEvent.Customer;
            OrderDate = salesOrderCreatedEvent.OrderDate;
            SubTotal = salesOrderCreatedEvent.SubTotal;
            Tax = salesOrderCreatedEvent.Tax;
            Total = salesOrderCreatedEvent.Total;
            DollarsOff = salesOrderCreatedEvent.DollarsOff;
            DiscountPercent = salesOrderCreatedEvent.DiscountPercent;
            Status = SalesOrderStatus.Open;

            Items = new List<OrderItem>();
            foreach (var itemEvent in salesOrderCreatedEvent.Items)
            {

                var id = Items.Count + 1;
                Items.Add(new OrderItem (
                    id,
                    itemEvent.Sku,
                    itemEvent.Quantity,
                    itemEvent.WholeSalePrice,
                    itemEvent.RetailPrice,
                    itemEvent.DollarsOff,
                    itemEvent.DiscountPercent,
                    itemEvent.Details));
            }
        }

        public SalesOrder(Guid id, SalesOrderEvents eventItems) : this(id)
        {
            LoadEvents(eventItems.Events);
        }

        public Guid AccountId { get; private set; }
        public Address ShippingAddress { get; private set; }
        public Address BillingAddress { get; private set; }
        public Person Customer { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total { get; private set; }
        public decimal DollarsOff { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public IList<OrderItem> Items { get; private set; }
        public SalesOrderStatus Status { get; private set; }
        public SalesOrderTypes OrderType { get; private set; }
        public Guid RefNo { get; private set; }
    }
}
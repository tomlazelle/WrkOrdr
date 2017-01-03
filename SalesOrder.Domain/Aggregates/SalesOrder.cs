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
            Handles<CreateReturnEvent>(ReturnCreated);
        }

        private void ReturnCreated(CreateReturnEvent createReturnEvent)
        {
            var id = _returns.Count + 1;
            _returns.Add(new CustomerReturn(id,
                createReturnEvent.Amount,
                createReturnEvent.Quantity,
                createReturnEvent.Sku,
                createReturnEvent.Reason,
                createReturnEvent.Action,
                createReturnEvent.Note,
                createReturnEvent.ReturnDate,
                createReturnEvent.ReturnId));

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

            _items = new List<OrderItem>();

            foreach (var itemEvent in salesOrderCreatedEvent.Items)
            {

                var id = Items.Count + 1;
                _items.Add(new OrderItem (
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

        private List<OrderItem> _items = new List<OrderItem>();
        private List<CustomerReturn> _returns = new List<CustomerReturn>();

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

        public IList<CustomerReturn> Returns
        {
            get
            {
                if (_returns == null) return _returns = new List<CustomerReturn>();

                return _returns.AsReadOnly();
            }
        }

        public IList<OrderItem> Items
        {
            get
            {
                if (_items == null) return _items = new List<OrderItem>();

                return _items.AsReadOnly();
            }
        }

        public SalesOrderStatus Status { get; private set; }
        public SalesOrderTypes OrderType { get; private set; }
        public Guid RefNo { get; private set; }
    }
}
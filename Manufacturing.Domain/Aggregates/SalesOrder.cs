using System;
using EventSource.Framework;
using Manufacturing.Domain.Events.SalesOrders;

namespace Manufacturing.Domain.Aggregates
{
    public class SalesOrder : BaseEntity<Guid>
    {
        public SalesOrder(Guid id) : base(id)
        {
            Handles<CreateSalesOrderEvent>(NewSalesOrder);
        }

        private void NewSalesOrder(CreateSalesOrderEvent createSalesOrderEvent)
        {
        }
    }
    public class SalesOrderAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace Manufacturing.Domain.Messages.SalesOrders
{
    public class CreateSalesOrderMessage
    {
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingCost { get; set; }
        public int StoreId { get; set; }
        public string DeliveryType { get; set; }
        public CreateAddress ShippingAddress { get; set; }
        public CreateAddress BillingAddress { get; set; }
        public IList<CreateSalesOrderItem> Items{ get; set; }
    }
    public class CreateSalesOrderItem
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public IDictionary<string,object> ItemDetails { get; set; }
        public decimal Discount { get; set; }

    }
    public class CreateAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
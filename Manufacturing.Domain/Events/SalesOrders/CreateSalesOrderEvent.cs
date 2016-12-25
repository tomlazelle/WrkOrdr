using System;
using EventSource.Framework;

namespace Manufacturing.Domain.Events.SalesOrders
{
    public class CreateSalesOrderEvent : VersionedEvent<Guid>
    {
        public DateTime CreateDate { get; }
        public string Status { get; }
        public decimal Total { get;  }
        public decimal SubTotal { get;  }
        public decimal Tax { get;  }
        public decimal Discount { get;  }
        public decimal ShippingCost { get;  }
        public int StoreId { get;  }
        public string DeliveryType { get;  }

    }
}
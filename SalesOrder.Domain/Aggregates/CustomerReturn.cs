using System;
using Sales.Common;

namespace Sales.Domain.Aggregates
{
    public class CustomerReturn
    {
        
        public CustomerReturn(int id, decimal amount, int quantity, string sku, ReturnReasons reason, ReturnAction action, string note, DateTime returnDate, string returnId)
        {            
            Id = id;
            Amount = amount;
            Quantity = quantity;
            Sku = sku;
            Reason = reason;
            Action = action;
            Note = note;
            ReturnDate = returnDate;
            ReturnId = returnId;
        }

        public string ReturnId { get; }
        public int Id { get; }
        public decimal Amount { get; }
        public int Quantity { get; }
        public string Sku { get; }
        public ReturnReasons Reason { get; }
        public ReturnAction Action { get; }
        public string Note { get; }
        public DateTime ReturnDate { get; }
    }
}
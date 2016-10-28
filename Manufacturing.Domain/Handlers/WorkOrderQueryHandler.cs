using System.Collections.Generic;
using System.Linq;
using Manufacturing.Domain.Indexes;
using Raven.Client;

namespace Manufacturing.Domain.Handlers
{
    public class WorkOrderQueryHandler
    {
        private readonly IDocumentStore _documentStore;

        public WorkOrderQueryHandler(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IList<WorkOrder> Get()
        {
            IList<WorkOrder> workOrders;

            using (var session = _documentStore.OpenSession())
            {
                
                

                workOrders = session
                    .Query<WorkOrderEvents>()
                    .Take(100)
                    .ToList()
                    .Select(x => new WorkOrder(x.Id, x))
                    .ToList();
                

            }

            return workOrders;
        }

        public WorkOrder GetByOrderId(int orderId)
        {
            using (var session = _documentStore.OpenSession())
            {
                return session.Query<WorkOrderIdIndex.EventTypeResult, WorkOrderIdIndex>()
                    .Where(x => x.OrderId == orderId)
                    .OfType<WorkOrderEvents>()
                    .ToList()
                    .Select(x => new WorkOrder(x.Id, x))
                    .FirstOrDefault();
            }
        }
    }

   
}
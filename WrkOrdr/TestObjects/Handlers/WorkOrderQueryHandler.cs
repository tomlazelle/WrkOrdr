using System.Collections.Generic;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;
using WrkOrdr.Configuration;
using WrkOrdr.TestObjects.Events;

namespace WrkOrdr.TestObjects.Handlers
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
                return session.Query<Zip.EventTypeResult, Zip>()
                    .Where(x => x.OrderId == orderId)
                    .OfType<WorkOrderEvents>()
                    .ToList()
                    .Select(x => new WorkOrder(x.Id, x))
                    .FirstOrDefault();
            }
        }
    }

   
}
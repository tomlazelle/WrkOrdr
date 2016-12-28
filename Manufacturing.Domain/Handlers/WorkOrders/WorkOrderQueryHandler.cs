using System;
using System.Collections.Generic;
using System.Linq;
using Manufacturing.Domain.Aggregates;
using Raven.Client;

namespace Manufacturing.Domain.Handlers.WorkOrders
{
    public class WorkOrderQueryHandler
    {
        private readonly IDocumentStore _documentStore;

        public WorkOrderQueryHandler(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IList<WorkOrder> Get(int pageIndex, int itemsPerPage)
        {
            IList<WorkOrder> workOrders;

            using (var session = _documentStore.OpenSession())
            {
                RavenQueryStatistics stats;

                workOrders = session
                    .Query<WorkOrderEvents>()
                    .Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                    .Statistics(out stats)
                    .Skip((pageIndex - 1)*itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList()
                    .Select(x => new WorkOrder(x.Id, x))
                    .ToList();
            }

            return workOrders;
        }

        public WorkOrder Get(Guid id)
        {
            using (var session = _documentStore.OpenSession())
            {
                var events = session.Load<WorkOrderEvents>("WorkOrderEvents/" + id);

                var wo = new WorkOrder(id, events);
                return wo;
            }
        }
    }

    //saved for ref only
    //                return session.Query<WorkOrderIdIndex.EventTypeResult, WorkOrderIdIndex>()
    //                    .Where(x => x.OrderId == orderId)
    //                    .OfType<WorkOrderEvents>()
    //                    .ToList()
    //                    .Select(x => new WorkOrder(x.Id, x))
    //                    .FirstOrDefault();
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Manufacturing.Api.Models;
using Manufacturing.Domain.Aggregates;
using Manufacturing.Domain.Handlers.WorkOrders;
using Manufacturing.Domain.Messages.WorkOrders;

namespace Manufacturing.Api.Controllers
{
    [RoutePrefix("v1/WorkOrder")]
    public class WorkOrderController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly WorkOrderHandler _workOrderHandler;
        private readonly WorkOrderQueryHandler _workOrderQueryHandler;

        public WorkOrderController(
            WorkOrderHandler workOrderHandler,
            WorkOrderQueryHandler workOrderQueryHandler,
            IMapper mapper)
        {
            _workOrderHandler = workOrderHandler;
            _workOrderQueryHandler = workOrderQueryHandler;
            _mapper = mapper;
        }

        [Route]
        public HttpResponseMessage Post(CreateWorkOrderMessage message)
        {
            var result = _workOrderHandler.Handle(message);

            if (result == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Work order not created!");
            }

            return Request.CreateResponse(HttpStatusCode.OK, result.Id);
        }

        [Route]
        public IEnumerable<WorkOrderModel> Get(int pageIndex, int itemsPerPage)
        {
            var workOrders = _workOrderQueryHandler.Get(pageIndex, itemsPerPage);

            var result = _mapper.Map<IEnumerable<WorkOrder>, IEnumerable<WorkOrderModel>>(workOrders);

            return result;
        }

        [Route("id")]
        public WorkOrderModel Get(Guid id)
        {
            var workOrders = _workOrderQueryHandler.Get(id);

            var result = _mapper.Map<WorkOrder, WorkOrderModel>(workOrders);

            return result;
        }
    }
}
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Manufacturing.Domain.Handlers;
using Manufacturing.Domain.Messages;

namespace Manufacturing.Api.Controllers
{

    [RoutePrefix("v1/WorkOrder")]
    public class WorkOrderController : ApiController
    {
        private readonly WorkOrderHandler _workOrderHandler;

        public WorkOrderController(WorkOrderHandler workOrderHandler)
        {
            _workOrderHandler = workOrderHandler;
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
    }
}
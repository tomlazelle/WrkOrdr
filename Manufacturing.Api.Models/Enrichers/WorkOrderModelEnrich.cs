using Manufacturing.Api.Models.Handlers;

namespace Manufacturing.Api.Models.Enrichers
{
    public class WorkOrderModelEnrich:ResponseEnricher<WorkOrderModel>
    {
        public WorkOrderModelEnrich(IMediaLinkFactory mediaLinkFactory)
            : base(mediaLinkFactory)
        {
        }

        public override void Enrich(WorkOrderModel content)
        {
            content.AddLink(CreateLink(MediaLinkType.Self, "WorkOrder",new {id=content.Id}));
            content.AddLink(CreateLink(MediaLinkType.Edit, "WorkOrder",new {id=content.Id}));
        }
    }
}
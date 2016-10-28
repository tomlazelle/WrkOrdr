using System;
using EventSource.Framework;

namespace Manufacturing.Domain.Handlers
{
    public class WorkOrderEvents : EventContainer
    {
        public WorkOrderEvents(Guid id) : base(id)
        {
        }
    }
}
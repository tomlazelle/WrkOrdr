using System;
using EventSource.Framework;

namespace WrkOrdr.TestObjects.Handlers
{
    public class WorkOrderEvents : EventContainer
    {
        public WorkOrderEvents(Guid id) : base(id)
        {
        }
    }
}
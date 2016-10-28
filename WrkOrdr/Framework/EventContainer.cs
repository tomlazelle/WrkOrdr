using System;
using System.Collections.Generic;
using System.Linq;

namespace WrkOrdr.Framework
{
    public abstract class EventContainer
    {
        protected EventContainer(Guid id)
        {
            Id = id;
        }

        public void AddEvent(IVersionedEvent<Guid> eventItem)
        {
            var list = new List<IVersionedEvent<Guid>>();

            if (Events != null)
            {
                list.AddRange(Events);
            }

            var version = list.Any() ? list.Count + 1 : 1;

            eventItem.Version = version;

            list.Add(eventItem);

            Events = list.ToArray();
        }

        public Guid Id { get; private set; }
        public IVersionedEvent<Guid>[] Events { get; private set; }
    }
}
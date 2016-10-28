using System;

namespace WrkOrdr.Framework
{
    public class VersionedEvent<TSourceId> : IVersionedEvent<TSourceId>
    {
        public TSourceId SourceId { get; internal set; }
        public DateTime When { get; internal set; }
        public string EventType { get; internal set; }
        public int Version { get; set; }

        public VersionedEvent()
        {
            When = DateTime.Now;
        }
    }
}
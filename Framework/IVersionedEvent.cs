namespace WrkOrdr.Framework
{
    public interface IVersionedEvent<out TSourceId> : IEvent<TSourceId>
    {
        int Version { get; set; }
    }
}
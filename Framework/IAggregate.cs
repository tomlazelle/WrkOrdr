namespace WrkOrdr.Framework
{
    public interface IAggregate<TId>
    {
        TId Id { get; }
    }
}
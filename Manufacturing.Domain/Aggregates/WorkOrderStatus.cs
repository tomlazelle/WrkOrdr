namespace Manufacturing.Domain.Aggregates
{
    public enum WorkOrderStatus
    {
        InProgress,
        NotStarted,
        Printing,
        Batching,
        UserCanceled,
        OnHold,
        BadArt
    }
}
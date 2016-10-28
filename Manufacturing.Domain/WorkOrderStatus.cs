namespace Manufacturing.Domain
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
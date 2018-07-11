namespace BehaviorTree
{
    /// <summary>
    /// Running status of each nodes.
    /// </summary>
    public enum NodeState
    {
        Unexecuted,
        Continue,
        Success,
        Failure,
    }
}

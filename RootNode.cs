using System.Diagnostics;

namespace BehaviorTree
{
    /// <summary>
    /// Root node.
    /// </summary>
    public class RootNode : InternalNode
    {
        public override void Run()
        {
            Debug.Assert(this.ChildNodes.Count == 1);

            if (this.Status == NodeState.Unexecuted)
                this.Status = NodeState.Continue;

            if (this.Status == NodeState.Continue)
                this.ChildNode.Run();

            this.Status = this.ChildNode.Status;
        }
    }
}

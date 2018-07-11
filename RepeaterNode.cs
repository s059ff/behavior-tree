using System.Diagnostics;

namespace BehaviorTree
{
    /// <summary>
    /// A node that represents a repeat processing.
    /// </summary>
    public class RepeaterNode : InternalNode
    {
        internal RepeaterNode() { }

        public override void Run()
        {
            Debug.Assert(this.ChildNodes.Count == 1);

            if (this.Status == NodeState.Unexecuted)
                this.Status = NodeState.Continue;
            
            if (this.Status == NodeState.Continue)
                this.ChildNode.Run();

            this.Status = this.ChildNode.Status;

            // After run, status of this node set to UNEXECUTED.
            if (this.Status == NodeState.Failure || this.Status == NodeState.Success)
                this.ResetStatus();
        }
    }
}

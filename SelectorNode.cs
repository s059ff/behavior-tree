using System.Collections.Generic;
using System.Diagnostics;

namespace BehaviorTree
{
    /// <summary>
    /// A node that represents a branch.
    /// </summary>
    public class SelectorNode : InternalNode
    {
        internal SelectorNode() { }

        /// <summary>
        /// A node whose status is CONTINUE. In the case of null, this node is not currently running.
        /// </summary>
        private IEnumerator<BehaviorNode> RunningNode;

        public override void Run()
        {
            // If child nodes is empty, this node returns SUCCESS code.
            if (this.ChildNodes.Count == 0)
            {
                this.Status = NodeState.Success;
                return;
            }

            // Set running node to first child node.
            for (this.RunningNode = this.ChildNodes.GetEnumerator(); this.RunningNode.MoveNext(); )
            {
                this.RunningNode.Current.Run();

                switch (this.RunningNode.Current.Status)
                {
                    // If running node returns CONTINUE, this node returns CONTINUE.
                    case NodeState.Continue:
                        this.Status = NodeState.Continue;
                        while (this.RunningNode.MoveNext())
                            this.RunningNode.Current.ResetStatus();
                        return;

                    // If running nodes returns SUCCESS, this node returns SUCCESS.
                    case NodeState.Success:
                        this.Status = NodeState.Success;
                        while (this.RunningNode.MoveNext())
                            this.RunningNode.Current.ResetStatus();
                        return;

                    // If running nodes returns FAILURE, continue the loop.
                    case NodeState.Failure:
                        continue;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }

            // If all nodes return FAILURE, this node returns FAILURE.
            this.Status = NodeState.Failure;
            return;
        }

        public override void ResetStatus()
        {
            this.RunningNode = null;
            base.ResetStatus();
        }
    }
}

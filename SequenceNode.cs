using System.Collections.Generic;
using System.Diagnostics;

namespace BehaviorTree
{
    /// <summary>
    /// A node that represents a sequential processing.
    /// </summary>
    public class SequenceNode : InternalNode
    {
        internal SequenceNode() { }

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

            // If running node was not initialized, set running node to first child node.
            if (this.RunningNode == null)
            {
                this.RunningNode = this.ChildNodes.GetEnumerator();
                this.RunningNode.MoveNext();
            }
            do
            {
                // Re-evaluate previous condition nodes.
                for (var it = this.ChildNodes.GetEnumerator(); it.MoveNext() && it.Current != this.RunningNode.Current;)
                {
                    if (it.Current.GetType() == typeof(ConditionNode))
                    {
                        // If evaluation value of previous condition node is not SUCCESS, then abort, returns FAILURE.
                        it.Current.Run();
                        if (it.Current.Status != NodeState.Success)
                        {
                            this.Status = NodeState.Failure;
                            while (this.RunningNode.MoveNext())
                                this.RunningNode.Current.ResetStatus();
                            this.RunningNode = null;
                            return;
                        }
                    }
                }

                this.RunningNode.Current.Run();

                switch (this.RunningNode.Current.Status)
                {
                    // If running node returns CONTINUE, this node returns CONTINUE.
                    case NodeState.Continue:
                        this.Status = NodeState.Continue;
                        while (this.RunningNode.MoveNext())
                            this.RunningNode.Current.ResetStatus();
                        this.RunningNode = null;
                        return;

                    // If running nodes returns SUCCESS, continue the loop.
                    case NodeState.Success:
                        continue;

                    // If running nodes returns FAILURE, this node returns FAILURE.
                    case NodeState.Failure:
                        this.Status = NodeState.Failure;
                        while (this.RunningNode.MoveNext())
                            this.RunningNode.Current.ResetStatus();
                        this.RunningNode = null;
                        return;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            while (this.RunningNode.MoveNext());

            // If all nodes return SUCCESS, this node returns SUCCESS.
            this.Status = NodeState.Success;
            this.RunningNode = null;
            return;
        }

        public override void ResetStatus()
        {
            this.RunningNode = null;
            base.ResetStatus();
        }
    }
}

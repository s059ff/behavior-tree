using System;
using System.Diagnostics;

namespace BehaviorTree
{
    /// <summary>
    /// A node that actually determines a behavior of the character.
    /// </summary>
    public class ActionNode : LeafNode
    {
        internal ActionNode(Func<NodeState> action)
        {
            this.action = action;
        }

        public override void Run()
        {
            this.Status = this.action();
            Debug.Assert(this.Status != NodeState.Unexecuted);
        }

        private Func<NodeState> action = () => { return NodeState.Continue; };
    }
}

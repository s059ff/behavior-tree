using System;

namespace BehaviorTree
{
    /// <summary>
    /// A node that represents a condition.
    /// </summary>
    public class ConditionNode : LeafNode
    {
        internal ConditionNode(Func<bool> condition)
        {
            this.condition = condition;
        }

        public override void Run()
        {
            this.Status = this.condition() ? NodeState.Success : NodeState.Failure;
        }

        private Func<bool> condition = () => { return true; };
    }
}

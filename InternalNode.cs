using System;

namespace BehaviorTree
{
    /// <summary>
    /// This node has some child nodes.
    /// </summary>
    public abstract class InternalNode : BehaviorNode
    {
        public SequenceNode Sequence()
        {
            var newNode = new SequenceNode();
            this.ChildNodes.Add(newNode);
            return newNode;
        }

        public SelectorNode Selector()
        {
            var newNode = new SelectorNode();
            this.ChildNodes.Add(newNode);
            return newNode;
        }

        public RepeaterNode Repeater()
        {
            var newNode = new RepeaterNode();
            this.ChildNodes.Add(newNode);
            return newNode;
        }

        public ActionNode Action(Func<NodeState> action)
        {
            var newNode = new ActionNode(action);
            this.ChildNodes.Add(newNode);
            return newNode;
        }

        public ConditionNode Condition(Func<bool> condition)
        {
            var newNode = new ConditionNode(condition);
            this.ChildNodes.Add(newNode);
            return newNode;
        }
    }
}

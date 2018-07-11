using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BehaviorTree
{
    /// <summary>
    /// Parent class of all node classes.
    /// </summary>
    public abstract class BehaviorNode
    {
        public BehaviorNode()
        {
            this.Status = NodeState.Unexecuted;
        }

        public virtual void Run()
        {
            throw new NotImplementedException();
        }

        public virtual void ResetStatus()
        {
            this.Status = NodeState.Unexecuted;

            foreach (var child in this.ChildNodes)
            {
                child.ResetStatus();
            }
        }

        public override string ToString()
        {
            var stream = new StringWriter();
            this.ToString(stream, 0);
            return stream.ToString();
        }

        public void ToString(StringWriter stream, int indentLevel = 0)
        {
            string text = string.Format("{0}: {1}", this.GetType().Name, this.Status);
            stream.WriteLine(new string(' ', indentLevel * 4) + text);
            foreach (var node in this.ChildNodes)
            {
                node.ToString(stream, indentLevel + 1);
            }
        }

        public NodeState Status { get; protected set; }

        protected List<BehaviorNode> ChildNodes = new List<BehaviorNode>();

        protected BehaviorNode ChildNode
        {
            get
            {
                Debug.Assert(this.ChildNodes.Count == 1);
                return this.ChildNodes.First();
            }
        }
    }
}

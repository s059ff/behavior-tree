using System;
using System.Collections.Generic;

namespace BehaviorTree.Test
{
    class Program
    {
        static void Main()
        {
#if false
            var container = new List<string>() { "a", "b", "c" };
            foreach (var item in container)
            {
                Console.WriteLine(item);
            }
            var it = container.GetEnumerator();

            bool reachedEnd = false;
            reachedEnd = it.MoveNext();
            Console.WriteLine(it.Current + ", " + reachedEnd);
            reachedEnd = it.MoveNext();
            Console.WriteLine(it.Current + ", " + reachedEnd);
            reachedEnd = it.MoveNext();
            Console.WriteLine(it.Current + ", " + reachedEnd);
            reachedEnd = it.MoveNext();
            Console.WriteLine(it.Current + ", " + reachedEnd);
            reachedEnd = it.MoveNext();
            Console.WriteLine(it.Current + ", " + reachedEnd);
#endif
#if false
            // Example 1.
            int x = 32;
            int y = 64;

            var root = new RootNode();
            var sequence = root.Sequence();
            var condition = sequence.Condition(() => x < y);
            var action = sequence.Action(() =>
            {
                Console.WriteLine("action.");
                x += 16;
                if (x < y)
                    return NodeState.Continue;
                else
                    return NodeState.Success;
            });

            for (int i = 0; i < 4; i++)
            {
                Console.Write(root);
                Console.WriteLine("x: {0}.", x);
                root.Run();
            }
#endif
#if false
            // Example 2.
            int x = 0;

            var root = new RootNode();
            // If does not attached repeator, always result is failure.
            var repeater = root.Repeater();
            var sequence = repeater.Sequence();
            //var sequence = root.Sequence();
            var condition1 = sequence.Condition(() => x == 0);
            var action1 = sequence.Action(() =>
            {
                if (x == 0)
                {
                    Console.WriteLine("Set x to 1.");
                    x = 1;
                    return NodeState.Success;
                }
                else
                {
                    return NodeState.Failure;
                }
            });
            var condition2 = sequence.Condition(() => x == 1);
            var action2 = sequence.Action(() =>
            {
                if (x == 1)
                {
                    Console.WriteLine("Set x to 2.");
                    x = 2;
                    return NodeState.Success;
                }
                else
                {
                    return NodeState.Failure;
                }
            });

            for (int i = 0; i < 4; i++)
            {
                Console.Write(root);
                Console.WriteLine("x: {0}.", x);
                root.Run();
            }
#endif
#if true
            // Example 3.
            int x = 0;

            var root = new RootNode();
            // If does not attached repeator, always result is failure.
            var repeater = root.Repeater();
            var selector = repeater.Selector();
            //var selector = root.Selector();
            var sequence1 = selector.Sequence();
            var condition1 = sequence1.Condition(() => x == 0);
            var action1 = sequence1.Action(() =>
            {
                Console.WriteLine("Set x is 1.");
                x = 1;
                return NodeState.Success;
            });
            var sequence2 = selector.Sequence();
            var condition2 = sequence2.Condition(() => x == 1);
            var action2 = sequence2.Action(() =>
            {
                Console.WriteLine("Set x is 0.");
                x = 0;
                return NodeState.Success;
            });

            for (int i = 0; i < 4; i++)
            {
                Console.Write(root);
                Console.WriteLine("x: {0}.", x);
                root.Run();
            }
#endif
#if false
            // Example 4. (Test for re-evaluation condition nodes)
            var root = new RootNode();
            var sequence = root.Sequence();
            var c1 = false;
            var c2 = false;
            sequence.Condition(() => c1);
            sequence.Condition(() => c2);
            sequence.Action(() =>
            {
                Console.WriteLine("A1");
                return NodeState.Continue;
            });

            c1 = true;
            c2 = true;
            Console.Write(root);
            Console.WriteLine("c1: {0}", c1);
            Console.WriteLine("c2: {0}", c2);
            root.Run();

            c1 = true;
            c2 = false;
            Console.Write(root);
            Console.WriteLine("c1: {0}", c1);
            Console.WriteLine("c2: {0}", c2);
            root.Run();

            c1 = false;
            c2 = true;
            Console.Write(root);
            Console.WriteLine("c1: {0}", c1);
            Console.WriteLine("c2: {0}", c2);
            root.Run();

            c1 = true;
            c2 = true;
            Console.Write(root);
            Console.WriteLine("c1: {0}", c1);
            Console.WriteLine("c2: {0}", c2);
            root.Run();
#endif
#if false
            // Example 5. (In the case of action node status is continue.)
            var root = new RootNode();
            //var repeater = root.Repeater();
            //var sequence = repeater.Sequence();
            var sequence = root.Sequence();
            var @continue = true;
            sequence.Action(() =>
            {
                Console.WriteLine("A1");
                return @continue ? NodeState.Continue : NodeState.Success;
            });
            sequence.Action(() =>
            {
                Console.WriteLine("A2");
                return NodeState.Failure;
            });

            @continue = true;
            Console.WriteLine("@continue: {0}", @continue);
            root.Run();
            Console.Write(root);

            @continue = false;
            Console.WriteLine("@continue: {0}", @continue);
            root.Run();
            Console.Write(root);

            @continue = true;
            Console.WriteLine("@continue: {0}", @continue);
            root.Run();
            Console.Write(root);
#endif
        }
    }
}

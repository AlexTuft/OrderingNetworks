using System.Collections.Generic;
using System.Linq;

namespace UpdatingFactories
{
    public class Node
    {
        public IList<Node> IncomingConnections { get; set; } = new List<Node>();
        public IList<Node> OutgoingConnections { get; set; } = new List<Node>();
    }

    public class Network
    {
        public IList<Node> Nodes { get; } = new List<Node>();

        public Node AddRootNode()
        {
            var node = new Node();
            Nodes.Add(node);
            return node;
        }

        public void Connect(Node from, Node to)
        {
            from.OutgoingConnections.Add(to);
            to.IncomingConnections.Add(from);

            var fromPosition = Nodes.IndexOf(from);
            var toPosition = Nodes.IndexOf(to);

            while (fromPosition > toPosition)
            {
                Nodes.Swap(fromPosition, toPosition);

                // to is now in the fromPosition
                CorrectForwardErrors(to, fromPosition);

                // from is now in the toPosition
                CorrectBackwardErrors(from, toPosition);

                fromPosition = Nodes.IndexOf(from);
                toPosition = Nodes.IndexOf(to);
            }
        }

        private void CorrectForwardErrors(Node from, int fromPosition)
        {
            /* We must correct any potential errors, as the original to node (now in the fromPosition) may be positioned
             * later in the list than any of its outgoing connections.
             * 
             * We essentially look for any outgoing connections of the node in the fromPosition. If any of those
             * connected nodes appear earlier in the list, we swap them again. We continue this until the node in the
             * fromPosition has no outgoing connections positioned before it in the list.
             */

            while (from.OutgoingConnections.Any())
            {
                var outgoingConnectionPositions = from.OutgoingConnections.Select(x => Nodes.IndexOf(x)).ToList();

                /* Assuming that the node in the fromPosition must be swapped with one of its outgoing connections,
                 * the connection selected for the swap is the earliest node in the list if there are multiple.
                 * By selecting the earliest node, we can ensure that it is updated before any of the other outgoing
                 * connections.
                 */
                var nextPosition = outgoingConnectionPositions.Count == 1 ? outgoingConnectionPositions.First() : outgoingConnectionPositions.Min();

                if (fromPosition > nextPosition)
                {
                    Nodes.Swap(fromPosition, nextPosition);
                    from = Nodes[fromPosition];
                }
                else
                {
                    // Everything is correct now
                    return;
                }
            }
        }

        private void CorrectBackwardErrors(Node to, int toPosition)
        {
            while (to.IncomingConnections.Any())
            {
                var incomingConnectionPositions = to.IncomingConnections.Select(x => Nodes.IndexOf(x)).ToList();

                var nextPosition = incomingConnectionPositions.Count == 1 ? incomingConnectionPositions.First() : incomingConnectionPositions.Max();

                if (toPosition < nextPosition)
                {
                    Nodes.Swap(toPosition, nextPosition);
                    to = Nodes[toPosition];
                }
                else
                {
                    // Everything is correct now
                    return;
                }
            }
        }
    }
}

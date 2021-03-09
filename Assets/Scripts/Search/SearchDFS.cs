using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SearchDFS
{
    public static bool Search(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
		bool found = false;

		Stack<GraphNode> nodes = new Stack<GraphNode>();
		nodes.Push(source);

		int steps = 0;
		while (!found && nodes.Count > 0 && steps++ < maxSteps)
		{
			GraphNode node = nodes.Peek();
			node.Visited = true;

			bool forward = false;
			foreach (GraphNode.Edge edge in node.Edges)
			{
				if (edge.nodeB.Visited == false)
				{
					nodes.Push(edge.nodeB);
					forward = true;
					if (edge.nodeB == destination)
					{
						found = true;
					}
					break;
				}
			}

			if (forward == false)
			{
				nodes.Pop();
			}
		}

		path = new List<GraphNode>(nodes);
		path.Reverse();
		
		return found;

	}
}

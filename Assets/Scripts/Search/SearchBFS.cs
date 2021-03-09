using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SearchBFS
{
    public static bool Search(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
		bool found = false;

		Queue<GraphNode> nodes = new Queue<GraphNode>();

		source.Visited = true;
		nodes.Enqueue(source);

		int steps = 0;
		while (!found && nodes.Count > 0 && steps++ < maxSteps)
		{
			GraphNode node = nodes.Dequeue();
			
			foreach (GraphNode.Edge edge in node.Edges)
			{
				if (edge.nodeB.Visited == false)
				{
					edge.nodeB.Visited = true;
					edge.nodeB.Parent = node;

					nodes.Enqueue(edge.nodeB);
				}
				if (edge.nodeB == destination)
				{
					found = true;
					break;
				}
			}
		}

		path = new List<GraphNode>();
		if (found)
		{
			GraphNode node = destination;
			while (node != null)
			{
				path.Add(node);
				node = node.Parent;
			}

			path.Reverse();
		}
		else
		{
			path = nodes.ToList();
		}

		return found;

	}
}

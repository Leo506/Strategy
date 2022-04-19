using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Following
{

    public class SearchInWidth
    {
        IGraph graph;
        Dictionary<IGraph, IGraph> globalPath;

        public SearchInWidth(IGraph graph)
        {
            this.graph = graph;
            globalPath = GetPath();
        }

        private Dictionary<IGraph, IGraph> GetPath()
        {
            Queue<IGraph> frontier = new Queue<IGraph>();
            frontier.Enqueue(graph);

            Dictionary<IGraph, IGraph> cameFrom = new Dictionary<IGraph, IGraph>();
            cameFrom.Add(graph, null);

            while (frontier.Count > 0)
            {
                IGraph current = frontier.Dequeue();

                foreach (var item in current.GetNeighnbours())
                {
                    if (!cameFrom.ContainsKey(item))
                    {
                        cameFrom.Add(item, current);
                    }
                }
            }

            return cameFrom;
        }

        public Vector2 GetNextPoint(Vector2 start, Vector2 end)
        {
            var startPoint = FindNearNode(start);
            var endPoint = FindNearNode(end);

            Debug.Log("Start point: " + startPoint.GetPosition());
            Debug.Log("End point: " + endPoint.GetPosition());

            return CreatePath(startPoint, endPoint)[1];
        }


        private IGraph FindNearNode(Vector2 target)
        {
            var near = globalPath.Keys.ElementAt(0);
            var nearDist = (target - near.GetPosition()).magnitude;

            foreach (var item in globalPath.Keys)
            {
                if ((target - item.GetPosition()).magnitude < nearDist)
                {
                    near = item;
                    nearDist = (target - near.GetPosition()).magnitude;
                }
            }

            return near;
        }


        private List<Vector2> CreatePath(IGraph start, IGraph end)
        {
            List<Vector2> path = new List<Vector2>();
            path.Add(end.GetPosition());

            var current = globalPath[end];

            while (current != null && current != start)
            {
                path.Add(current.GetPosition());
                current = globalPath[current];
            }

            path.Add(start.GetPosition());

            path.Reverse();

            return path;

        }
    }
}
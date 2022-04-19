using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Following
{

    public class GraphNode : MonoBehaviour, IGraph
    {
        [SerializeField] List<GraphNode> neighnbours;

        public List<IGraph> GetNeighnbours()
        {
            List<IGraph> toReturn = new List<IGraph>();
            foreach (GraphNode node in neighnbours)
                toReturn.Add(node as IGraph);
            
            return toReturn;
        }

        public Vector2 GetPosition()
        {
            return this.transform.position;
        }

        private void OnDrawGizmos()
        {
            if (neighnbours == null)
                return;

            foreach (var item in neighnbours)
            {
                Gizmos.DrawLine(transform.position, item.GetPosition());
            }
        }
    }
}
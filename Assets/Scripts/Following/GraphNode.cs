using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Following
{

    public class GraphNode : MonoBehaviour, IGraph
    {
        [SerializeField] List<Transform> neighnbours;

        public List<Vector2> GetNeighnbours()
        {
            List<Vector2> toReturn = new List<Vector2>();
            foreach (var item in neighnbours)
            {
                toReturn.Add(item.position);
            }
            return toReturn;
        }

        private void OnDrawGizmos()
        {
            if (neighnbours == null)
                return;

            foreach (var item in neighnbours)
            {
                Gizmos.DrawLine(transform.position, item.position);
            }
        }
    }
}
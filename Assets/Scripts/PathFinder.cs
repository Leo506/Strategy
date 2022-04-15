using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    public Dictionary<Vector3Int, Vector3Int?> GetPath(Vector3 startPos, Vector3 endPos)
    {
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        frontier.Enqueue(tilemap.WorldToCell(startPos));

        Dictionary<Vector3Int, Vector3Int?> cameFrom = new Dictionary<Vector3Int, Vector3Int?>();
        cameFrom.Add(tilemap.WorldToCell(startPos), null);

        Vector3Int endPoint = tilemap.WorldToCell(endPos);

        while (frontier.Count > 0)
        {
            Vector3Int currentPoint = frontier.Dequeue();

            if (currentPoint == endPoint)
                break;

            foreach (var item in GetNeighbors(currentPoint))
            {
                if (!cameFrom.ContainsKey(item))
                {
                    //Debug.Log("Adding to came from: " + item);
                    frontier.Enqueue(item);
                    cameFrom.Add(item, currentPoint);
                }
            }
        }

        return cameFrom;
    }

    private Vector3Int[] GetNeighbors(Vector3Int point)
    {
        bool isEven = point.y % 2 == 0;

        Vector3Int?[] neighbors =
        {
            point + new Vector3Int(+1, 0, 0),
            point + new Vector3Int(0, -1, 0),
            point + new Vector3Int(+1, -1, 0),
            point + new Vector3Int(-1, 0, 0),
            point + new Vector3Int(0, +1, 0),
            point + new Vector3Int(isEven ? 0 : +1, +1, 0)
        };

        for (int i = 0; i < neighbors.Length; i++)
        {
            if (!tilemap.HasTile(neighbors[i].Value))
                neighbors[i] = null;
        }

        return neighbors.Where(x => x != null).Select(x => x.Value).ToArray();
    }
}

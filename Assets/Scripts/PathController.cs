using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class PathController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    public List<Vector3> GetPath(Vector3 startPos, Vector3 endPos)
    {
        var path = SearchInWidth(startPos, endPos);
        return ReversePath(path, endPos);
    }


    // Реализация поиска в ширину
    private Dictionary<Vector3Int, Vector3Int?> SearchInWidth(Vector3 startPos, Vector3 endPos)
    {
        startPos = new Vector3(startPos.x, startPos.y, 0);
        endPos = new Vector3(endPos.x, endPos.y, 0);

        // Фронт
        Queue<Vector3Int> frontier = new Queue<Vector3Int>();
        frontier.Enqueue(tilemap.WorldToCell(startPos));

        // Словарь, чтобы запомнить откуда мы пришли к каждому узлу
        Dictionary<Vector3Int, Vector3Int?> cameFrom = new Dictionary<Vector3Int, Vector3Int?>();
        cameFrom.Add(tilemap.WorldToCell(startPos), null);

        Vector3Int endPoint = tilemap.WorldToCell(endPos);


        // Пока фронт не закончился
        while (frontier.Count > 0)
        {
            Vector3Int currentPoint = frontier.Dequeue();     // Достаем очередной узел графа

            if (currentPoint == endPoint)                     // Если этот узел совпадает с требуемым - останавливаемся
                break;

            foreach (var item in GetNeighbors(currentPoint))  // Проверяем всех соседей узла
            {
                if (!cameFrom.ContainsKey(item))              // Если узел не был обработан (не добавлен в словарь пути)
                {
                    frontier.Enqueue(item);                   // Добавляем узел во фронт
                    cameFrom.Add(item, currentPoint);         // Отмечаем из какого узла(currentPoint) пришли в проверяемый (item)
                }
            }
        }

        return cameFrom;
    }

    private List<Vector3> ReversePath(Dictionary<Vector3Int, Vector3Int?> path, Vector3 endPos)
    {
        endPos = new Vector3(endPos.x, endPos.y, 0);
        List<Vector3Int> tempRes = new List<Vector3Int>();

        Vector3Int? current = tilemap.WorldToCell(endPos);
        tempRes.Add(current.Value);
        //Debug.Log("Current: " + current + " Value: " + path[current.Value]);

        current = path[current.Value];
        //Debug.Log("Current: " + current + " Value: " + path[current.Value]);

        while (current != null)
        {
            tempRes.Add(current.Value);
            current = path[current.Value];
            //Debug.Log("Current: " + current + " Value: " + (current == null ? "" : path[current.Value].ToString()));
        }

        List<Vector3> result = new List<Vector3>();
        for (int i = tempRes.Count - 1; i >= 0; i--)
        {
            result.Add(tilemap.CellToWorld(tempRes[i]));
            //Debug.Log("Temp: " + tempRes[i] + " Result: " + tilemap.CellToWorld(tempRes[i]));
        }

        return result;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] PathFinder finder;
    [SerializeField] Unit unit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("World pos: " + worldPos + " Tilemap pos: " + tilemap.WorldToCell(worldPos));

            Vector3Int end = tilemap.WorldToCell(worldPos);
            end = new Vector3Int(end.x, end.y, 0);

            var path = Reverse(finder.GetPath(unit.gameObject.transform.position, worldPos), end);
            unit.Move(path);
        }
    }

    private List<Vector3> Reverse(Dictionary<Vector3Int, Vector3Int?> path, Vector3Int endPos)
    {
        List<Vector3Int> tempRes = new List<Vector3Int>();

        Vector3Int? current = endPos;
        tempRes.Add(current.Value);

        current = path[current.Value];

        while(current != null)
        {
            tempRes.Add(current.Value);
            current = path[current.Value];
        }

        List<Vector3> result = new List<Vector3>();
        for (int i = tempRes.Count - 1; i >= 0; i--)
        {
            result.Add(tilemap.CellToWorld(tempRes[i]));
        }

        return result;
    }
}
